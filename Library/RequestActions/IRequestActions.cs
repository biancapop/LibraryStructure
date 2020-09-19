using System.Collections.Generic;

namespace Library.RequestActions
{
    public interface IRequestActions
    {

        List<string> Do();
        void Process();
        void Revert();

        string GetPetitionId();
    }
}
