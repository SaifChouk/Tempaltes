using System;
using System.Threading;

namespace Correlation
{
    public class CorrelationAccessor : ICorrelationAccessor
    {
        private static readonly AsyncLocal<string> CurrentStorage = new AsyncLocal<string>();

        public string GetId()
        {
            return CurrentStorage.Value ?? (CurrentStorage.Value = CreateId());
        }

        public void SetId(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            CurrentStorage.Value = id;
        }

        private string CreateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
