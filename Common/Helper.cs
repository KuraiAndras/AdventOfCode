﻿using static System.Console;

namespace Common;

public static class Helper
{
    public static async Task<string> LoadPart(int index) => await File.ReadAllTextAsync($"Data{index}.txt");
    public static async Task<string[]> LoadPartLines(int index) => await File.ReadAllLinesAsync($"Data{index}.txt");

    public static void Answer<T>(int part, T answer) => WriteLine($"Part {part} answer is: {answer}");
}
