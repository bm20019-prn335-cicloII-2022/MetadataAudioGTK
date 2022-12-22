using System;
using System.IO;

namespace MetadataAudio.Datos;

public class MetadataLib
{
    public PropiedadesTag CreateFile(string pathFile)
    {
        if (!File.Exists(pathFile))
            throw new FileNotFoundException("El archivo no existe: \""+pathFile+"\"");
        
        PropiedadesTag pt = new PropiedadesTag();
        try
        { 
            TagLib.File file = TagLib.Image.File.Create(pathFile); 
            //TAG
            pt.Title = file.Tag.Title; 
            pt.Subtitle = file.Tag.Subtitle; 
            pt.Album = file.Tag.Album; 
            pt.Pictures = file.Tag.Pictures; 
            pt.Performers = file.Tag.JoinedPerformers; 
            pt.Genres = file.Tag.JoinedGenres; 
            pt.Copyright = file.Tag.Copyright;
            pt.Track = (int)file.Tag.Track;
            pt.Year =(int)file.Tag.Year;
            pt.ISRC = file.Tag.ISRC;
            pt.Disc = (int)file.Tag.Disc;
            pt.Composers = file.Tag.JoinedComposers;
            pt.TrackCount =(int) file.Tag.TrackCount;
            //Propiedades
            pt.Duracion = file.Properties.Duration.ToString("hh':'mm':'ss");
            pt.AudioSampleRate = file.Properties.AudioSampleRate;
            pt.Codecs = file.Properties.Codecs;
            return pt;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
