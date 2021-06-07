using Composite.Interfaces;

namespace Composite
{
    public class Corporation : BookComposite
    {
        public Corporation(string name) : base(name: name) { }
        protected override string HeadingTagName => "h1";
    }
    public class Store : BookComposite
    {
        public Store(string name) : base(name) { }
        protected override string HeadingTagName => "h2";
    }
    public class Section : BookComposite
    {
        public Section(string name) : base(name) { }
        protected override string HeadingTagName => "h3";
    }
    public class Set : BookComposite
    {
        public Set(string name, params IComponent[] books) : base(name)
        {
            foreach (IComponent book in books)
            {
                Add(book);
            }
        }
        protected override string HeadingTagName => "h4";
    }
}
