using System.Collections.Generic;

namespace SaveSystem
{
    public interface ISaveable<T> where T: class
    {
        string SaveId { get; }

        T Save();
    }
}