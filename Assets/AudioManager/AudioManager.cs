using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    public static float effectsVolumn = 1f;
    public static float voiceVolumn = 1f;

    public static IDictionary<string, AudioClip> Bgms = new Dictionary<string, AudioClip>();
    public static IDictionary<int, IDictionary<string, AudioClip>> Cvs = new Dictionary<int, IDictionary<string, AudioClip>>();
    
    public AudioClip loadCharacterCVByResource(int charID, string clipname)
    {
        if (CvDicGetSafe(charID,clipname) != null)
            return CvDicGetSafe(charID, clipname);
        AudioClip cv = Resources.Load("Audios/characterCV/" + charID.ToString() + "/" + clipname, typeof(AudioClip)) as AudioClip;
        CvDicAddSafe(charID,clipname,cv);
        return cv;
    }
    
    public AudioClip CvDicGetSafe(int charID, string clipname)
    {
        if (!Cvs.ContainsKey(charID))
            return null;
        if (Cvs[charID] == null)
            return null;
        if (!Cvs[charID].ContainsKey(clipname))
            return null;
        return Cvs[charID][clipname];
    }
    
    public void CvDicAddSafe(int charID, string clipname,AudioClip _clip)
    {
        if (Cvs.ContainsKey(charID))
        {
            if (Cvs[charID] != null)
            {
                if (Cvs[charID].ContainsKey(clipname))
                {
                    Cvs[charID][clipname] = _clip;
                }else{
                    Cvs[charID].Add(clipname,_clip);
                }
            }else{
                Cvs[charID] = new Dictionary<string, AudioClip>();
                Cvs[charID].Add(clipname,_clip);
            }
        }else{
            Cvs.Add(charID,new Dictionary<string, AudioClip>());
            Cvs[charID].Add(clipname,_clip);
        }
    }
    
    public AudioClip loadBgmByResource(string clipname)
    {
        if (Bgms.ContainsKey(clipname))
            return Bgms[clipname];
        AudioClip bgm = Resources.Load("Audios/bgm/" + clipname.ToString(), typeof(AudioClip)) as AudioClip;
        Bgms.Add(clipname,bgm);
        return bgm;
    }
}
