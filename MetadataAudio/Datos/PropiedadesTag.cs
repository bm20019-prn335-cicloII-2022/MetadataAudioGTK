using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagLib;

namespace MetadataAudio.Datos;

public class PropiedadesTag
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Performers { get; set; }
    public string Composers { get; set; }
    public string Album { get; set; }
    public string Genres { get; set; }
    public int Year { get; set; }
    public int Track { get; set; }
    public int TrackCount { get; set; }
    public int Disc { get; set; }
    public string Copyright { get; set; }
    public string ISRC { get; set; }
    public IPicture[] Pictures { get; set; }
    //
    public string Duracion { get; set; }
    public int AudioSampleRate { get; set; }
    public IEnumerable<ICodec> Codecs { get; set; }

    public Stream[] GetPicture()
    {
        if (GetCodecsCount() == 0)
            return null;
        List<Stream> lstR = new List<Stream>();
        foreach (IPicture IMG in Pictures)
        {
            Picture p = new Picture(IMG.Data);
            MemoryStream ms = new MemoryStream(p.Data.Data);
            lstR.Add(ms);
        }
        return lstR.ToArray();
    }

    public int GetCodecsCount()
    {
        return Codecs.Count();
    }
    public string JoinCodecs()
    {
        string code = "";
        foreach (ICodec VARIABLE in Codecs)
        {
            code += $"{VARIABLE.ToString()}\n";
        }
        return code.Remove(code.Length-1);
    }
}