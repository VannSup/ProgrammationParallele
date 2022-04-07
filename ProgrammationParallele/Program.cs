using ProgrammationParallele;

var imageTool = new ImageTool();

var imagesInfo = new DirectoryInfo(imageTool.filePathSource)?.GetFiles();

ParallelTraietment(imagesInfo);
SingleTraietment(imagesInfo);

/// <summary>
/// Traitement parallel des images
/// </summary>
async void ParallelTraietment(FileInfo[] imageInfos)
{
    Console.WriteLine("----------- Start threaded traitement -----------");
    var watch = System.Diagnostics.Stopwatch.StartNew();

    List<Task> tasks = new List<Task>();

    foreach (var item in imageInfos)
    {
        tasks.Add(Task.Run(() =>
        {
            imageTool.Imagetraitement(item.Name, "tread");
            Console.WriteLine($"Start threaded traitement on {item.Name} in Thread :{Thread.CurrentThread.ManagedThreadId}");
        }));
    }

    Task.WaitAll(tasks.ToArray());

    watch.Stop();
    var elapsedMs = watch.ElapsedMilliseconds;
    Console.WriteLine($"----------- End threaded traitement in {elapsedMs} ms-----------");
}

/// <summary>
/// Taritement linéaire des images
/// </summary>
void SingleTraietment(FileInfo[] imageInfos)
{
    Console.WriteLine("----------- Start single traitement -----------");
    var watch = System.Diagnostics.Stopwatch.StartNew();

    foreach (var item in imageInfos)
    {
        Console.WriteLine($"Start single traitement on {item.Name}");
        imageTool.Imagetraitement(item.Name);
    }

    watch.Stop();
    var elapsedMs = watch.ElapsedMilliseconds;
    Console.WriteLine($"----------- End single traitement in {elapsedMs} ms -----------");
}