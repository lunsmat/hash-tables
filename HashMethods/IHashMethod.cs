namespace HashMethods
{
    public interface IHashMethod
    {
        public string GetHashName();
        public void Print();
        public void Insert(int key);
        public void Insert(string key);
        public void Remove(int key);
        public void Remove(string key);
        public void Search(int key);
        public void Search(string key);
        public void Restructure(int newTableSize);
        public void Clear();
    }
}