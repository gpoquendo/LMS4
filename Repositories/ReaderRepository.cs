using LMS3.Models;

namespace LMS3.Repositories
{
    public class ReaderRepository
    {
        public static List<Reader> _readerList = new List<Reader>()
        {
            new Reader { ReaderId = 1, Name = "John Doe" },
            new Reader { ReaderId = 2, Name = "Jane Doe" },
            new Reader { ReaderId = 3, Name = "Alice" }
        };

        public static List<Reader> GetAllReaders()
        {
            return _readerList;
        }

        public static Reader? GetReaderById(int id)
        {
            var reader = _readerList.FirstOrDefault(r => r.ReaderId == id);
            if (reader != null)
            {
                return reader;
            }
            return null;
        }

        public static void AddReader(Reader reader)
        {
            var maxId = _readerList.Max(r => r.ReaderId);
            reader.ReaderId = maxId + 1;
            _readerList.Add(reader);
        }

        public static void UpdateReader(Reader reader)
        {
            var existingReader = _readerList.FirstOrDefault(r => r.ReaderId == reader.ReaderId);
            if (existingReader != null)
            {
                existingReader.Name = reader.Name;
            }
        }
        
        public static void DeleteReader(int id)
        {
            var reader = _readerList.FirstOrDefault(r => r.ReaderId == id);
            if (reader != null)
            {
                _readerList.Remove(reader);
            }
        }
    }
}