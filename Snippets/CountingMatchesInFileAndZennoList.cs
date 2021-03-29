var searchWord = "текст";                              // искомый текст.
var pathFile = @"C:\Users\ILYA\Desktop\test.txt";   // файл в котором ищем.
var zennoList = project.Lists["Мой зенно список"];    // зенно список в котором ищем.
 
{
    /*Получение количества строк в которых встречается заданный текст (пример с файлом)*/
    var text = new List<string>(File.ReadAllLines(pathFile).Where(x => !string.IsNullOrWhiteSpace(x)));
    var _counter = 0;
    
    if (!string.IsNullOrWhiteSpace(searchWord) && text.Count != 0) text.ForEach(x => {if (x.Contains(searchWord)) _counter++;});
 
    project.SendToLog($"FILE | Количество строк в которых встречается заданный текст: {_counter}", ZennoLab.InterfacesLibrary.Enums.Log.LogType.Info);
}
 
{
    /*Получение количества совпадений в которых встречается заданный текст (пример с файлом)*/
    var text = new List<string>(Regex.Matches(File.ReadAllText(pathFile), @"\b[a-zA-Z0-9а-яА-Я]{2,}\b").Cast<Match>().Select(x => x.Value));
    var _counter = 0;
    
    if (!string.IsNullOrWhiteSpace(searchWord) && text.Count != 0) text.ForEach(x => {if (x.Contains(searchWord)) _counter++;});
 
    project.SendToLog($"FILE | Количество раз встречается заданный текст: {_counter}", ZennoLab.InterfacesLibrary.Enums.Log.LogType.Info);
}
 
{
    /*Получение количества строк в которых встречается заданный текст (пример с зенно списком)*/
    var _counter = 0;
    
    if (!string.IsNullOrWhiteSpace(searchWord) && zennoList.Count != 0) zennoList.ToList().ForEach(x => {if (x.Contains(searchWord)) _counter++;});
 
    project.SendToLog($"ZENNO_LIST | Количество строк в которых встречается заданный текст: {_counter}", ZennoLab.InterfacesLibrary.Enums.Log.LogType.Info);
}
 
{
    /*Получение количества совпадений в которых встречается заданный текст (пример с зенно списком)*/
    var text = new List<string>(Regex.Matches(string.Join("\n", zennoList), @"\b[a-zA-Z0-9а-яА-Я]{2,}\b").Cast<Match>().Select(x => x.Value));
    var _counter = 0;
    
    if (!string.IsNullOrWhiteSpace(searchWord) && text.Count != 0) text.ForEach(x => {if (x.Contains(searchWord)) _counter++;});
 
    project.SendToLog($"ZENNO_LIST | Количество раз встречается заданный текст: {_counter}", ZennoLab.InterfacesLibrary.Enums.Log.LogType.Info);
}
