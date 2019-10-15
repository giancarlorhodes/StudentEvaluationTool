using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEvaluationToolCommon
{
    public interface IHash
    {

        string FoldingHash(string value);
        string SHA256HasingNoSalt(string value);
        string SHA256HasingWithSalt(string value, string salt, bool IsSkip = false);


    }
}
