
public class CachDownLoadMission
{
    public string filename;
    public string subPath;
    public float filesize;
    public bool downloadfinished;
    
    public CachDownLoadMission(string subPath,string filename, float filesize)
    {
        this.filename = filename;
        this.subPath = subPath;
        this.filesize = filesize;
    }
}