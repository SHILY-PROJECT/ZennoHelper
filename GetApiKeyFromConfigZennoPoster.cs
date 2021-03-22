//Сниппет №1:
string Service = "RuCaptcha.dll"; // указать *.dll сервиса (sms или капча серивс - без разницы)
 
string path_config = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\ZennoLab\Configs\{Service}.config";
string api_key = Regex.Match(File.ReadAllText(path_config), "(?<=add\\ key=\"key\"\\ value=\").*?(?=\")").Value;
 
if (String.IsNullOrWhiteSpace(api_key))
    throw new Exception($"В настройках ZennoPoster отсутствует API KEY от сервиса: {Service}");
 
project.SendInfoToLog($"API_KEY: {api_key}");
 
//Сниппет №2 (разницы почти нет):
string Service = "RuCaptcha.dll"; // указать *.dll сервиса (sms или капча серивс - без разницы)
 
string path_config = Environment.ExpandEnvironmentVariables($@"%AppData%\ZennoLab\Configs\{Service}.config");
string api_key = Regex.Match(File.ReadAllText(path_config), "(?<=add\\ key=\"key\"\\ value=\").*?(?=\")").Value;
 
if (String.IsNullOrWhiteSpace(api_key))
    throw new Exception($"В настройках ZennoPoster отсутствует API KEY от сервиса: {Service}");
 
project.SendInfoToLog($"API_KEY: {api_key}");
