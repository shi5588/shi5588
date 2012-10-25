
using System;
using System.IO;
using System.Security.Cryptography;

namespace SHA256
{
    public class HMACSHA256example
    {
        // Computes a keyed hash for a source file, creates a target file with the keyed hash
        // prepended to the contents of the source file, then decrypts the file and compares
        // the source and the decrypted files.
        public static void EncodeFile(byte[] key, String sourceFile, String destFile)
        {
            // Initialize the keyed hash object.
            HMACSHA256 myhmacsha256 = new HMACSHA256(key);
            FileStream inStream = new FileStream(sourceFile, FileMode.Open);
            FileStream outStream = new FileStream(destFile, FileMode.Create);
            // Compute the hash of the input file.
            byte[] hashValue = myhmacsha256.ComputeHash(inStream);
            // Reset inStream to the beginning of the file.
            inStream.Position = 0;
            // Write the computed hash value to the output file.
            outStream.Write(hashValue, 0, hashValue.Length);
            // Copy the contents of the sourceFile to the destFile.
            int bytesRead;
            // read 1K at a time
            byte[] buffer = new byte[1024];
            do
            {
                // Read from the wrapping CryptoStream.
                bytesRead = inStream.Read(buffer, 0, 1024);
                outStream.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);
            myhmacsha256.Clear();
            // Close the streams
            inStream.Close();
            outStream.Close();
            return;
        } // end EncodeFile


        // Decrypt the encoded file and compare to original file.
        public static bool DecodeFile(byte[] key, String sourceFile)
        {
            // Initialize the keyed hash object. 
            HMACSHA256 hmacsha256 = new HMACSHA256(key);
            // Create an array to hold the keyed hash value read from the file.
            byte[] storedHash = new byte[hmacsha256.HashSize / 8];
            // Create a FileStream for the source file.
            FileStream inStream = new FileStream(sourceFile, FileMode.Open);
            // Read in the storedHash.
            inStream.Read(storedHash, 0, storedHash.Length);
            // Compute the hash of the remaining contents of the file.
            // The stream is properly positioned at the beginning of the content, 
            // immediately after the stored hash value.
            byte[] computedHash = hmacsha256.ComputeHash(inStream);
            // compare the computed hash with the stored value
            for (int i = 0; i < storedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i])
                {
                    Console.WriteLine("Hash values differ! Encoded file has been tampered with!");
                    return false;
                }
            }
            Console.WriteLine("Hash values agree -- no tampering occurred.");
            return true;
        } //end DecodeFile

        private const string usageText = "Usage: HMACSHA256 inputfile.txt encryptedfile.hsh\nYou must specify the two file names. Only the first file must exist.\n";
        public static void Main(string[] Fileargs)
        {
            //If no file names are specified, write usage text.
            if (Fileargs.Length < 2)
            {
                Console.WriteLine(usageText);
            }
            else
            {
                try
                {
                    // Create a random key using a random number generator. This would be the
                    //  secret key shared by sender and receiver.
                    byte[] secretkey = new Byte[64];
                    //RNGCryptoServiceProvider is an implementation of a random number generator.
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    // The array is now filled with cryptographically strong random bytes.
                    rng.GetBytes(secretkey);

                    // Use the secret key to encode the message file.
                    EncodeFile(secretkey, Fileargs[0], Fileargs[1]);

                    // Take the encoded file and decode
                    DecodeFile(secretkey, Fileargs[1]);
                }
                catch (IOException e)
                {
                    Console.WriteLine("Error: File not found", e);
                }
            } //end if-else

        }  //end main
    } //end class

}