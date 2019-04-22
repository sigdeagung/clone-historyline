public static class Mediator
{
    static private Dictionary<Type, Type> dic = new Dictionary<Type, Type>();

    static Mediator()
    {
        RegisterApp(new MediaApplication());
    }

    static void RegisterApp(IApplication oApp)
    {
        dic.Add(oApp.GetSupportedApplicationType(), oApp.GetType());
    }

    static public void Open(IFile file)
    {
        Type appType = dic[file.GetType()];
        if (appType == null)
        {
            Console.WriteLine("No application was assigned to open up " + file);
            return;
        }

        IApplication app = (IApplication)System.Activator.CreateInstance(appType);
        app.OpenFile(file);
    }
}

 public interface IFile<T>
    {
        void Open(T path);
    }

    public abstract class Application
    {
        //public abstract void Open();
    }


    public class TextViewer : Application, IFile<TextFile>
    {
        public void Open(TextFile path)
        {
            //open textfile....
        }
    }

    public class MediaPlayer : Application, IFile<MediaFile>
    {     
        public void Open(MediaFile path)
        {
            //open media file...
        }
    }

    public class ImageViewer : Application, IFile<ImageFile>
    {
        public void Open(ImageFile path)
        {
            //open imagefile....
        }
    }