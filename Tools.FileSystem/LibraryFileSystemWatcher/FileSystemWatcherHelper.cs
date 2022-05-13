using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFileSystemWatcher
{
    public class FileSystemWatcherHelper
    {
        //专门定义了一个FileChangeInformation类来记录文件变化信息，
        //并定义了一个CustomQueue类，该类类似于Queue类，是一个数据先进先出的集合，
        //用来存储所有的文件变化消息，并提供数据持久化功能
        public void run()
        {
            string _path = "";
            string _filter = ".txt";

            FileSystemWatcher  _watcher = new FileSystemWatcher(_path, _filter);
            //注册监听事件，以及编写事件触发后相关的处理逻辑。


            //_watcher.Created += new FileSystemEventHandler(OnChanged);
            //_watcher.Changed += new FileSystemEventHandler(OnChanged);
            //_watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //_watcher.Renamed += new RenamedEventHandler(OnRenamed);


            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;
        }



    }
}
