/*-----------------------------------------------------------
  Работа с IZennoList (как я обычно с ними взаимодействую)
*///---------------------------------------------------------
var MyZennoList = project.Lists["Мой зенно список"];
 
//Удалить все строки удовлетворяющие регулярному выражению.
for (int i = 0; i <  MyZennoList.Count;)
    if (Regex.IsMatch(MyZennoList[i], @"наша регулярка")) MyZennoList.RemoveAt(i); else i++;
 
//Удалить все строки не удовлетворяющие регулярному выражению.
for (int i = 0; i <  MyZennoList.Count;)
    if (!Regex.IsMatch(MyZennoList[i], @"наша регулярка")) MyZennoList.RemoveAt(i); else i++;
 
//Удалить все пустые строк и состоящие из пробелов или null.
for (int i = 0; i <  MyZennoList.Count;)
    if (String.IsNullOrWhiteSpace(MyZennoList[i])) MyZennoList.RemoveAt(i); else i++;
 
//Удалить все строки которые содержат заданный текст (удаление по не точному совпадению).
for (int i = 0; i <  MyZennoList.Count;)
    if (MyZennoList[i].Contains("заданный текст")) MyZennoList.RemoveAt(i); else i++;
 
//Удалить все строки которые не содержат заданный текст (удаление по не точному совпадению).
for (int i = 0; i <  MyZennoList.Count;)
    if (!MyZennoList[i].Contains("заданный текст")) MyZennoList.RemoveAt(i); else i++;
 
//Удалить все строки которые содержат заданный текст (удаление по точному значению).
for (int i = 0; i <  MyZennoList.Count;)
    if (MyZennoList[i].Equals("заданный текст", StringComparison.Ordinal)) MyZennoList.RemoveAt(i); else i++;
 
//Удалить все строки которые не содержат заданный текст (удаление по точному значению).
for (int i = 0; i <  MyZennoList.Count;)
    if (!MyZennoList[i].Equals("заданный текст", StringComparison.Ordinal)) MyZennoList.RemoveAt(i); else i++;
 
//P.S. Всё, что выше из примеров приведено - мой дурной тон, мне так больше нравится (на работу никак не влияет).
 
/*-------------------------------------------------------------------------------------*/
//Пример более правильной записи циклов:
for (int i = 0; i <  MyZennoList.Count;)
{
    if (!MyZennoList[i].Contains("заданный текст"))
    {
        MyZennoList.RemoveAt(i);
    }
    else
    {
        i++;
    }
}
 
//Так тоже будет правильно:
for (int i = 0; i <  MyZennoList.Count;)
{
    if (!MyZennoList[i].Contains("заданный текст"))
        MyZennoList.RemoveAt(i);
    else
        i++;
}
 
/*-------------------------------------------------------------------------------------*/
//Можно конечно и так, но я просто не люблю, когда итератор инициализируется вне цикла.
int j = 0;
while (true)
{
    if (j == MyZennoList.Count || MyZennoList.Count == 0) break;
    if (MyZennoList[j].Equals("заданный текст", StringComparison.Ordinal))
    {
        MyZennoList.RemoveAt(j);
    }
    else
    {
        j++;
    }
}
 
/*-------------------------------------------------------------------------------------*/
//Рекомендую на многопотоке обернуть циклы локом, чтоб снизить вероятность словить исключение (если конечно список привязан к файлу).
lock (SyncObjects.ListSyncer)
{
    for (int i = 0; i <  MyZennoList.Count;)
        if (MyZennoList[i].Equals("заданный текст", StringComparison.Ordinal)) MyZennoList.RemoveAt(i); else i++;
}
