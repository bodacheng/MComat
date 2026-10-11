#!/usr/bin/env python3
"""Run the project's Unity skill authoring pipeline without a cloud service or extra packages."""
import argparse
import json
import pathlib
import subprocess
import sys


def main():
    project = pathlib.Path(__file__).resolve().parents[1]
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("action", choices=("analyze", "validate", "create", "verify", "selftest"))
    parser.add_argument("--plan", help="Plan JSON path, relative to the project or absolute")
    parser.add_argument("--clip", help="Unity Assets path of an .anim or FBX")
    parser.add_argument("--clip-name", help="Exact subclip name; required if the FBX contains several clips")
    parser.add_argument("--rig", help="Unity Assets path of the runtime character prefab")
    parser.add_argument("--unity", help="Path to the Unity editor executable")
    args = parser.parse_args()
    if args.action != "selftest" and not args.plan:
        parser.error("--plan is required")
    if args.action == "analyze" and (not args.clip or not args.rig):
        parser.error("analyze requires --clip and --rig")
    version = next(line.split(": ", 1)[1] for line in
                   (project / "ProjectSettings/ProjectVersion.txt").read_text().splitlines()
                   if line.startswith("m_EditorVersion: "))
    unity = pathlib.Path(args.unity) if args.unity else pathlib.Path(
        f"/Applications/Unity/Hub/Editor/{version}/Unity.app/Contents/MacOS/Unity")
    if not unity.is_file():
        parser.error(f"Unity {version} not found; pass --unity with the matching editor executable")
    logs = project / "Logs"
    logs.mkdir(exist_ok=True)
    report = logs / f"skill-authoring-{args.action}.json"
    log = logs / f"skill-authoring-{args.action}.log"
    # Never interpret a previous successful run as the result of a failed editor launch.
    report.unlink(missing_ok=True)
    command = [str(unity), "-batchmode", "-nographics", "-projectPath", str(project),
               "-executeMethod", "SkillAuthoringBatch.Run", "-skillAction", args.action,
               "-skillOutput", str(report), "-logFile", str(log)]
    for value, flag in ((args.plan, "-skillPlan"), (args.clip, "-skillClip"),
                        (args.clip_name, "-skillClipName"), (args.rig, "-skillRig")):
        if value:
            command.extend((flag, value))
    print(f"Unity {version}: {args.action}. Log: {log}", flush=True)
    completed = subprocess.run(command, cwd=project, check=False)
    if report.exists():
        result = json.loads(report.read_text())
        print(json.dumps(result, ensure_ascii=False, indent=2))
        return 0 if completed.returncode == 0 and result.get("success") else 1
    print(f"Unity did not produce a result. Inspect {log}. Close this project's open editor before batch mode.", file=sys.stderr)
    return completed.returncode or 1


if __name__ == "__main__":
    raise SystemExit(main())
