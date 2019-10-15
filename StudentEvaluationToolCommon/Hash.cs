namespace StudentEvaluationToolCommon
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    //// <summary>
    /// Name:           Giancarlo Rhodes 
    /// Company:        Onshore Outsourcing
    /// Description:    Using the for hasting passwords  
    /// </summary>
    public class Hash : IHash
    {

        public string FoldingHash(string value)
        {
            Int32 _runningInteger = 0;

            string hashedValue = String.Empty;
            //var crypt = new SHA256Managed();
            //byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(value));
            int index = 0;
            byte[] b = new byte[value.Length];
            b = Encoding.ASCII.GetBytes(value);
            byte[] fourBytes = new byte[4];

            foreach (byte theByte in b)
            {
                // take 4 bytes and turn into integer

                fourBytes[index] = theByte;

                if (index == 3)
                {

                    // convert it
                    int _tempInteger = BitConverter.ToInt32(fourBytes, 0);
                    _runningInteger = _runningInteger + _tempInteger;

                    // reset index
                    index = 0;
                    fourBytes[0] = 0;
                    fourBytes[1] = 0;
                    fourBytes[2] = 0;
                    fourBytes[3] = 0;

                }

                index++;
            }

            // check in an remaining bytes and concat if there are
            if (fourBytes[0] != 0)
            {

                int _tempInteger = BitConverter.ToInt32(fourBytes, 0);
                _runningInteger = _runningInteger + _tempInteger;

            }



            hashedValue = _runningInteger.ToString();

            return hashedValue;
        }

        public string SHA256HasingNoSalt(string value)
        {

            string hashedValue = String.Empty;
            var crypt = new SHA256Managed();

            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(value));
            foreach (byte theByte in crypto)
            {
                hashedValue += theByte.ToString("x2");
            }

            return hashedValue;
        }

        /// <summary>
        ///  Description:    SHA256 with salt
        /// </summary>
        /// <param name="value"></param>
        /// <param name="salt"></param>
        /// <param name="isskip"></param>
        /// <returns></returns>
        public string SHA256HasingWithSalt(string value, string salt, bool isskip)
        {
            // declaring variables - unuseful and unnecessary comment
            string hashedValue = "";
            byte[] plainTextByteArray = new byte[value.Length];        
            byte[] saltByteArray = new byte[salt.Length];
            byte[] plainTextWithSaltBytes = new byte[plainTextByteArray.Length + salt.Length];

            HashAlgorithm algorithm = new SHA256Managed();

            if (isskip == false) // return hashed value
            {
                // get the bytes into their byte arrays
                plainTextByteArray = Encoding.ASCII.GetBytes(value);
                saltByteArray = Encoding.ASCII.GetBytes(salt);

                // at to final array
                for (int i = 0; i < plainTextByteArray.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainTextByteArray[i];
                }

                // add to the final array
                for (int i = 0; i < salt.Length; i++)
                {
                    // notice that indexer using the end for our first array as the starting location
                    plainTextWithSaltBytes[plainTextByteArray.Length + i] = saltByteArray[i];
                }

                // hash it SHA256 style !!!! as a byte[] array
                var _hashed = algorithm.ComputeHash(plainTextWithSaltBytes);

                // btye[] to string
                hashedValue = System.Text.Encoding.Default.GetString(_hashed);

            }
            else  // return unhashed value
            {
                hashedValue = value;
            }
          
            return hashedValue;
        }

    }
}
