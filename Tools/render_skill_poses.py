#!/usr/bin/env python3
"""Render an analysis pose sheet for review. Requires Pillow (available in Codex's bundled Python)."""
import argparse
import json
import math
from pathlib import Path
from PIL import Image, ImageDraw, ImageFont


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("plan")
    parser.add_argument("output")
    args = parser.parse_args()
    plan = json.loads(Path(args.plan).read_text())
    report = plan["analysis"]
    frames = report["poseFrames"]
    if not frames:
        parser.error("No pose evidence. Analyze the source clip first.")
    names, parents = report["poseBoneNames"], report["poseParents"]
    columns, width, height, header = 4, 420, 370, 95
    font_path = "/System/Library/Fonts/Supplemental/Arial.ttf"
    font = ImageFont.truetype(font_path, 17) if Path(font_path).exists() else ImageFont.load_default()
    rows = math.ceil(len(frames) / columns)
    canvas = Image.new("RGB", (columns * width, rows * height + header), "#101827")
    draw = ImageDraw.Draw(canvas)
    draw.text((24, 14), report["clipName"] + " | sampled skeleton evidence", fill="white", font=font)
    draw.text((24, 42), "Blue = left limbs   Orange = right limbs   Gray = trunk   View: front / side", fill="#cbd5e1", font=font)
    draw.text((24, 68), "Poses use a fixed root orientation; hips translation is removed. Collision and range still require battle testing.", fill="#94a3b8", font=font)
    extent = max(1.0, max(abs(v[axis]) for f in frames for v in f["positions"] for axis in ("x", "y", "z")))
    scale = min(90 / extent, 105)
    for index, frame in enumerate(frames):
        col, row = index % columns, index // columns
        x, y = col * width, row * height + header
        draw.rectangle((x + 8, y + 8, x + width - 8, y + height - 8), fill="#172336")
        draw.text((x + 22, y + 18), f'{frame["time"]:.3f} s', font=font, fill="white")
        active = any(h["enabled"] and h["startTime"] <= frame["time"] <= h["endTime"] for h in plan["hits"])
        if active:
            draw.text((x + 240, y + 18), "ACTIVE", font=font, fill="#86efac")
        for view, axis in enumerate(("x", "z")):
            cx, cy = x + 110 + view * 200, y + 195
            draw.line((cx - 88, cy, cx + 88, cy), fill="#334155", width=1)
            points = [(cx + p[axis] * scale, cy - p["y"] * scale) for p in frame["positions"]]
            for bone, (point, parent) in enumerate(zip(points, parents)):
                name = names[bone].lower()
                color = "#60a5fa" if "left" in name else "#fb923c" if "right" in name else "#cbd5e1"
                if parent >= 0:
                    draw.line((points[parent], point), fill=color, width=4)
                px, py = point
                draw.ellipse((px - 3, py - 3, px + 3, py + 3), fill=color)
            draw.text((cx - 20, y + height - 40), "front" if view == 0 else "side", font=font, fill="#94a3b8")
    Path(args.output).parent.mkdir(parents=True, exist_ok=True)
    canvas.save(args.output)


if __name__ == "__main__":
    main()
