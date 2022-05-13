CSharp监控文件系统更改

本文主要描述如何通过C＃实现实时监控文件目录下的变化，包括文件和目录的添加，删除，修改和重命名等操作。
首先，我们需要对.net提供的FileSystemWatcher类有所了解。我有些懒，找了MSDN对该类的描述。
FileSystemWatcher类侦听文件系统更改通知，并在目录或目录中的文件发生更改时引发事件。

使用 FileSystemWatcher 监视指定目录中的更改。可监视指定目录中的文件或子目录的更改。
可以创建一个组件来监视本地计算机、网络驱动器或远程计算机上的文件。

若要监视所有文件中的更改，请将 Filter 属性设置为空字符串 ("") 或使用通配符（“*.*”）。
若要监视特定的文件，请将 Filter 属性设置为该文件名。例如，若要监视文件 MyDoc.txt 中的更改，
请将 Filter 属性设置为“MyDoc.txt”。也可以监视特定类型文件中的更改。
例如，若要监视文本文件中的更改，请将 Filter 属性设置为“*.txt”。

可监视目录或文件中的若干种更改。例如，可监视文件或目录的 Attributes、LastWrite 日期和时间或 Size 方面的更改。
通过将 NotifyFilter 属性设置为 NotifyFilters 值之一来达到此目的。
有关可监视的更改类型的更多信息，请参见 NotifyFilters。
可监视文件或目录的重命名、删除或创建。例如，若要监视文本文件的重命名，请将 Filter 属性设置为“*.txt”，
并使用为其参数指定的 Renamed 来调用 WaitForChanged 方法。

Windows 操作系统在 FileSystemWatcher 创建的缓冲区中通知组件文件发生更改。
如果短时间内有很多更改，则缓冲区可能会溢出。这将导致组件失去对目录更改的跟踪，并且它将只提供一般性通知。
使用 InternalBufferSize 属性来增加缓冲区大小的开销较大，因为它来自无法换出到磁盘的非页面内存，
所以应确保缓冲区大小适中（尽量小，但也要有足够大小以便不会丢失任何文件更改事件）。
若要避免缓冲区溢出，请使用 NotifyFilter 和 IncludeSubdirectories 属性，以便可以筛选掉不想要的更改通知。

使用 FileSystemWatcher 类时，请注意以下事项。
1） 对包括隐藏文件(夹)在内的所有文件(夹)进行监控。
2） 您可以为 InternalBufferSize 属性（用于监视网络上的目录）设置的最大大小为 64 KB。
FileSystemWatcher的实例监控到文件(夹)的变化后，会触发相应的事件，其中文件(夹)的添加，
删除和修改会分别触发Created，Deleted，Changed事件，文件(夹)重命名时触发OnRenamed事件。