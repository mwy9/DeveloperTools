namespace LibraryFileSystemWatcher
{
    public class FileChangeInformation
    {
        private string strGuid;
        public FileChangeType fileChangeType;
        private string strOldFullPath;
        private string strFullPath;
        private string strOldName;
        public string strName;
        


        public FileChangeInformation(string strGuid, FileChangeType fileChangeType,
            string strOldFullPath, string strFullPath, string strOldName, string strName)
        {
            this.strGuid = strGuid;
            this.fileChangeType = fileChangeType;
            this.strOldFullPath = strOldFullPath;
            this.strFullPath = strFullPath;
            this.strOldName = strOldName;
            this.strName = strName; 
        }

    }
}