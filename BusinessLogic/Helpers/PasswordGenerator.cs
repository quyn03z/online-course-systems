using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
	public static class PasswordGenerator
	{
		private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
		private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private const string Digits = "0123456789";
		private const string AllChars = Lowercase + Uppercase + Digits;

		public static string GeneratePassword(int length = 8)
		{
			if (length < 6)
				throw new ArgumentException("Password phải >= 6 ký tự");

			var password = new StringBuilder();

			// Bắt buộc mỗi loại 1 ký tự
			password.Append(GetRandomChar(Lowercase));
			password.Append(GetRandomChar(Uppercase));
			password.Append(GetRandomChar(Digits));

			// Các ký tự còn lại
			for (int i = password.Length; i < length; i++)
			{
				password.Append(GetRandomChar(AllChars));
			}

			// Xáo trộn lại cho random
			return Shuffle(password.ToString());
		}

		private static char GetRandomChar(string chars)
		{
			byte[] buffer = new byte[4];
			RandomNumberGenerator.Fill(buffer);
			int num = BitConverter.ToInt32(buffer, 0) & int.MaxValue;
			return chars[num % chars.Length];
		}

		private static string Shuffle(string input)
		{
			var array = input.ToCharArray();
			for (int i = array.Length - 1; i > 0; i--)
			{
				byte[] buffer = new byte[4];
				RandomNumberGenerator.Fill(buffer);
				int j = BitConverter.ToInt32(buffer, 0) & int.MaxValue % (i + 1);

				(array[i], array[j]) = (array[j], array[i]);
			}
			return new string(array);
		}
	}
}
