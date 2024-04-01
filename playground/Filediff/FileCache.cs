
namespace Filediff
{
    internal class FileCacheInfo  : IEquatable<FileCacheInfo>
    {
        public string Name { get; set; }
        public byte[] Hash { get; set; }

        public override bool Equals(object obj) 
            => obj is FileCacheInfo other && Equals(other);

        public bool Equals(FileCacheInfo other)
            => Hash.SequenceEqual(other.Hash);

        public override int GetHashCode()
            => Hash.GetHashCode();
    }
}
