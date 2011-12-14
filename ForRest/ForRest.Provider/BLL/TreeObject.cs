
namespace ForRest.Provider.BLL
{
    public class TreeObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public readonly ITree<string> TextTree;
        public readonly ITree<double> NumericTree;

        public TreeObject(string name, string type, ITree<string> textTree)
        {
            Name = name;
            Type = type;
            TextTree = textTree;
            NumericTree = null;
        }

        public TreeObject(string name, string type, ITree<double> numericTree)
        {
            Name = name;
            Type = type;
            TextTree = null;
            NumericTree = numericTree;
        }
    }
}
