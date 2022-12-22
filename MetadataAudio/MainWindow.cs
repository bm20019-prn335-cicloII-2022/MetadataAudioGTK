using System;
using System.IO;
using Gdk;
using Gtk;
using MetadataAudio.Datos;
using Application = Gtk.Application;
using UI = Gtk.Builder.ObjectAttribute;
using Window = Gtk.Window;

namespace MetadataAudio
{
    class MainWindow : Window
    {
        [UI] private Label lblTitulo;
        [UI] private Image lblImagen;
        [UI] private Label lblAlbum;
        [UI] private Label lblInterpretes;
        [UI] private Label lblGenero;
        [UI] private Label lblYear;
        [UI] private Label lblTrack;
        [UI] private Label lblCopyright;
        [UI] private Label lblTrackCount;
        [UI] private Label lblDiscCount;
        [UI] private Label lblComposers;
        [UI] private Label lblISRC;
        [UI] private Label lblDuracion;
        [UI] private Label lblCodecs;
        [UI] private Label lblAudioSample;
        public string _FilePath { get; set; }
        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }
        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            if(Environment.GetCommandLineArgs().Length>1)
                _FilePath = Environment.GetCommandLineArgs()[1];
            builder.Autoconnect(this);
            this.Title = $"Metadata: - {new FileInfo(_FilePath).Name}";
            DeleteEvent += Window_DeleteEvent;

            if (File.Exists(_FilePath))
            {
                Console.WriteLine("Procesando...");
                MetadataLib ml = new MetadataLib();
                PropiedadesTag pt = ml.CreateFile(_FilePath);
                //Tag
                lblTitulo.Text = $"Titulo: {pt.Title}";
                lblAlbum.Text = $"Album: {pt.Album}";
                lblInterpretes.Text = $"Interpretes: {pt.Performers}";
                lblGenero.Text = $"Generos: {pt.Genres}";
                lblYear.Text = $"Año : {pt.Year}";
                lblTrack.Text = $"Pista N°: {pt.Track}";
                lblCopyright.Text = $"Copyright: {pt.Copyright}";
                lblTrackCount.Text = $"Numero de Pistas: {pt.TrackCount}";
                lblDiscCount.Text = $"Disco Numero: {pt.Disc}";
                lblComposers.Text = $"Compositores: {pt.Composers}";
                lblISRC.Text = $"ISRC: {pt.ISRC}";
                //Propiedades
                lblDuracion.Text = $"Duracion: {pt.Duracion}";
                lblCodecs.Text = $"Codecs: {pt.JoinCodecs()}";
                lblAudioSample.Text = $"AudioSample: {pt.AudioSampleRate}";
                
                try
                {
                    Stream sm = new MemoryStream();
                    sm = pt.GetPicture()[0];
                    if (sm != null)
                        lblImagen.Pixbuf = new Pixbuf(sm, 250, 250);
                }
                catch (Exception e)
                {
                    
                }
                Console.WriteLine("Procesamiento Completado");
            }
            else
            {
                throw new FileNotFoundException($"El archivo No existe {_FilePath}");
            }
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
    }
}