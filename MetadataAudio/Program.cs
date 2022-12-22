using System;
using System.Linq;
using Gtk;

namespace MetadataAudio
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();
            var app = new Application("org.MetadataAudio.MetadataAudio", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);
            var win = new MainWindow();
            app.AddWindow(win); 
            win.Show(); 
            Application.Run();
        }
    }
}