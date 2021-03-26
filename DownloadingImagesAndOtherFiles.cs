/*Скачать файл средствами ZennoPoster с исходным именем*/
{
    string url = "https://avatanplus.com/files/resources/original/58f1ea455683515b70fb1eea.png";         // урл файла, который качаем.
    string downloadPath = $@"{project.Directory}\Скачать файл средствами ZennoPoster с исходным именем"; // путь, куда будет скачен файл.
    
    var HttpResponse = ZennoPoster.HTTP.Request
    (
        ZennoLab.InterfacesLibrary.Enums.Http.HttpMethod.GET, url, "", "", "", "UTF-8",
        ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.File, 30000, "", project.Profile.UserAgent, true, 5, 
        null, downloadPath, false, false, project.Profile.CookieContainer
    );
}
 
/*Скачать файл средствами ZennoPoster с переименованием*/
{
    string url = "https://avatanplus.com/files/resources/original/58f1ea455683515b70fb1eea.png";         // урл файла, который качаем.
    string downloadPath = $@"{project.Directory}\Скачать файл средствами ZennoPoster с переименованием"; // путь, куда будет скачен файл.
    string newNameFile = "Файл с новым именем";                                                          // новое имя файла.
    
    var HttpResponse = ZennoPoster.HTTP.Request
    (
        ZennoLab.InterfacesLibrary.Enums.Http.HttpMethod.GET, url, "", "", "", "UTF-8",
        ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.File, 30000, "", project.Profile.UserAgent, true, 5, 
        null, downloadPath, false, false, project.Profile.CookieContainer
    );
    
    var fi = new FileInfo($@"{downloadPath}\{Path.GetFileName(url)}");
    var newPathFile = Path.Combine(fi.DirectoryName, newNameFile + fi.Extension);
    
    try
    {
        if (!fi.Exists) throw new Exception($"Файла не существует: {fi.FullName}");
        File.Move(fi.FullName, newPathFile);
    }
    catch(Exception ex)
    {
        project.SendWarningToLog(ex.Message);
    }
}
 
/*Скачать файл средствами WebClient с новым именем*/
{
    string url = "https://avatanplus.com/files/resources/original/58f1ea455683515b70fb1eea.png";    // урл файла, который качаем.
    string nameFile = "Файл с новым именем";                                                        // имя файла.
    string downloadPath = $@"{project.Directory}\Скачать файл средствами WebClient с новым именем"; // путь, куда будет скачен файл.
    
    if (!Directory.Exists(downloadPath)) Directory.CreateDirectory(downloadPath);                   // проверяем папку на существование (создаем, если её нет).
    
    using (var wc = new System.Net.WebClient()) File.WriteAllBytes($@"{downloadPath}\{nameFile}{Path.GetExtension(url)}", wc.DownloadData(url));
}
 
/*Скачать файл средствами WebClient с исходным именем*/
{
    string url = "https://avatanplus.com/files/resources/original/58f1ea455683515b70fb1eea.png";        // урл файла, который качаем.
    string downloadPath = $@"{project.Directory}\Скачать файл средствами WebClient с исходным именем";  // путь, куда будет скачен файл.
    
    if (!Directory.Exists(downloadPath)) Directory.CreateDirectory(downloadPath);                       // проверяем папку на существование (создаем, если её нет).
    
    using (var wc = new System.Net.WebClient()) wc.DownloadFile(url, Path.Combine(downloadPath, Path.GetFileName(url)));
}
