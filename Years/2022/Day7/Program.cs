using System.Collections.Immutable;

var inputLines = (await LoadPartLines(1)).Skip(1).ToImmutableArray();

var currentDirectory = new DirectoryEntry(string.Empty, "/");
var directories = new List<DirectoryEntry>() { currentDirectory };

foreach (var line in inputLines)
{
    if (line.StartsWith("$"))
    {
        if (line.StartsWith("$ cd "))
        {
            var targetSubDirectoryName = line.Replace("$ cd ", string.Empty);

            if(targetSubDirectoryName != "..")
            {
                var targetSubDirectoryFullName = currentDirectory.FullPath + targetSubDirectoryName + "/";

                currentDirectory = directories.Find(d => d.FullPath == targetSubDirectoryFullName) ?? throw new InvalidOperationException("Moving into unknown directory");
            }
            else
            {
                currentDirectory = directories.Find(d => d.SubDirectories.Any(sd => sd.FullPath == currentDirectory.FullPath)) ?? throw new InvalidOperationException("Could not find parent directory");
            }
        }
    }
    else
    {
        var parts = line.Split(' ');
        if (parts[0] == "dir")
        {
            var subDirectory = directories.Find(d => d.FullPath == currentDirectory.FullPath + parts[0]);
            if (subDirectory is null)
            {
                subDirectory = new DirectoryEntry
                (
                    parts[1],
                    currentDirectory.FullPath + parts[1] + "/"
                );
                directories.Add(subDirectory);
            }

            if (!currentDirectory.SubDirectories.Any(d => d.FullPath == subDirectory.FullPath))
            {
                currentDirectory.SubDirectories.Add(subDirectory);
            }
        }
        else
        {
            currentDirectory.Files.Add(new FileEntry(parts[1], int.Parse(parts[0])));
        }
    }
}

var answer1 = directories.Select(d => d.TotalSize()).Where(s => s <= 100000).Sum();

Answer(1, answer1);

record DirectoryEntry(string Name, string FullPath)
{
    public List<FileEntry> Files { get; } = new();
    public List<DirectoryEntry> SubDirectories { get; } = new();

    public int SizeOfFiles() => Files.Select(f => f.Size).Sum();

    public int TotalSize() => SizeOfFiles() + SubDirectories.Select(d => d.TotalSize()).Sum();
}

record FileEntry(string Name, int Size);
