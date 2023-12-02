using OneOf;

namespace Day13;

//public abstract class PacketData
//{

//}
//public class IntData : PacketData
//{
//    public int Value { get; set; }
//}
//public class ListData : PacketData
//{
//    public List<PacketData> Value { get; set; }
//}

[GenerateOneOf]
public partial class PacketData : OneOfBase<int, List<PacketData>> { }
