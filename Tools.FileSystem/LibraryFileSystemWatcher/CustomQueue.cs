using System;
using System.Collections;

namespace LibraryFileSystemWatcher
{
    public enum FileChangeType
    {
        Unknow,
        NewFolder,
        NewFile,
        Delete,
        Change,
        Rename
    }

    public class CustomQueue<T> : Queue
    {
        private bool isPersistence;
        private string persistenceFilePath;


        public CustomQueue()
        {

        }
        public CustomQueue(bool isPersistence, string persistenceFilePath)
        {
            this.isPersistence = isPersistence;
            this.persistenceFilePath = persistenceFilePath;
        }

        internal object SelectAll(Func<FileChangeInformation, bool> p)
        {
            throw new NotImplementedException();
        }

        //Queue<string> ts = new Queue<string>();
        //ts.Dequeue();
        //ts.Select
    }
}