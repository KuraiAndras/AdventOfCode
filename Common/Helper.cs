namespace Common;

public static class Helper
{
    public static async Task<string> LoadDay(int index) => await File.ReadAllTextAsync($"Data{index}.txt");
    public static async Task<string[]> LoadDayLines(int index) => await File.ReadAllLinesAsync($"Data{index}.txt");
}
