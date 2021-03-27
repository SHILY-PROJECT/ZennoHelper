/*--------------------------------
        Данные для запроса
*///------------------------------
string _account = ""; // аккаунт через разделитель "login:pass" или "login;pass".
string _proxy = "";
 
/*----------------------------------------------------
        Авторизация mail.ru
*///---------------------------------------------------
string _login = String.Empty, _password = String.Empty;
try
{
    _login = _account.Split(':', ';')[0];
    _password = _account.Split(':', ';')[1];
    
    if (String.IsNullOrWhiteSpace(_login)) throw new Exception("Логин не указан");
    if (String.IsNullOrWhiteSpace(_password)) throw new Exception("Пароль не указан");
    
    project.Profile.CookieContainer.Clear(); // очистка контейнера куков.
    
    string HttpResponse = ZennoPoster.HTTP.Request
    (
        ZennoLab.InterfacesLibrary.Enums.Http.HttpMethod.GET, "https://mail.ru/", "", "", _proxy, "UTF-8",
        ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.BodyOnly, 20000, "", project.Profile.UserAgent, true, 5,
        new String[]{},  "", false, false, project.Profile.CookieContainer
    );
    if (String.IsNullOrWhiteSpace(HttpResponse)) throw new Exception("HttpResponse пуст");
 
    string token = Regex.Match(HttpResponse, "(?<=CSRF:\").*?(?=\")").Value; // парсинг токена для авторизации.
    
    if (String.IsNullOrWhiteSpace(token)) throw new Exception("token для авторизации не найден");
    
    /*Авторизация*/
    HttpResponse = ZennoPoster.HTTP.Request
    (
        ZennoLab.InterfacesLibrary.Enums.Http.HttpMethod.POST, "https://auth.mail.ru/jsapi/auth",
        $"login={ZennoLab.Macros.TextProcessing.UrlEncode(_login)}&password={ZennoLab.Macros.TextProcessing.UrlEncode(_password)}&saveauth=1&token={token}&project=e.mail.ru&_="
        + Convert.ToString((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).Replace(",", "").Substring(0, 13),
        "application/x-www-form-urlencoded", _proxy, "UTF-8",
        ZennoLab.InterfacesLibrary.Enums.Http.ResponceType.HeaderAndBody, 20000, "", project.Profile.UserAgent, true, 5,
        new String[]
        {
            "Accept: */*",
            "Origin: https://mail.ru",
            "Referer: https://mail.ru/",
            "Connection: keep-alive"
        },
        "", false, false, project.Profile.CookieContainer
    );
    if (Regex.IsMatch(HttpResponse, "(?<=\\{\"status\":\")ok.*?(?=\"})"))
    {
        project.Profile.Save($@"{project.Directory}\profiles\{_login}.zpprofile", true, true, true, true, true, true, true, true, true, new string[]{"PROXY"}); // сейв профиля.
        project.SendInfoToLog($"{_login} | Успешная авторизация", true); // лог для zp.
    }
    else
    {
        /*Определение ошибки, если не вышло авторизоваться*/
        if (!Regex.IsMatch(HttpResponse, "((?<=\\{\"status\":\")break.*?(?=\"})|(?<=\"status\":\")fail.*?(?=\"}))")) throw new Exception("Неизвестная ошибка авторизации");
        if (Regex.IsMatch(HttpResponse, "(?<=\\{\"status\":\")break.*?(?=\"})")) throw new Exception("Требуется SMS подтверждение/Восстановление доступа");
        if (Regex.IsMatch(HttpResponse, "(?<=\"status\":\")fail.*?(?=\"})")) throw new Exception("Не верный логин или пароль");
    }
}
catch (Exception ex)
{
    project.SendWarningToLog(!String.IsNullOrWhiteSpace(_login) ? $"{_login} | {ex.Message}" : $"{ex.Message}", true);
}
