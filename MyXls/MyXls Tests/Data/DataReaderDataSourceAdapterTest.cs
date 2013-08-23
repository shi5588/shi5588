using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework;

namespace org.in2bits.MyXls.Data
{
	[TestFixture]
	public class DataReaderDataSourceAdapterTest
	{
		private TestDataReader _reader;

		[SetUp]
		public void Setup()
		{
			List<string> columns = new List<string> { "Id", "Name", "Age" };
			List<object[]> rows = new List<object[]>
                 {
                 	new object[] { Guid.NewGuid(), "Mickey Mouse", 52 },
					new object[] { Guid.NewGuid(), null, 48 },
                 	new object[] { Guid.NewGuid(), "Goofy", 51 },
                 	new object[] { Guid.NewGuid(), "Donald Duck", 49 }
                 };
			_reader = new TestDataReader(columns, rows);
		}

		[Test]
		public void GetEnumerator()
		{
			DataReaderDataSourceAdapter<IDataReader> adapter = new DataReaderDataSourceAdapter<IDataReader>(_reader);
			IEnumerable enumerable = adapter.GetEnumerator();
			Assert.IsNotNull(enumerable);

			int i = 0;
			foreach (IDataReader reader in enumerable)
			{
				object[] row = GetRow(reader);
				CollectionAssert.AreEqual(_reader.Rows[i++], row);
			}

			Assert.AreEqual(4, i, "Should have read 4 rows.");
		}

		[Test]
		public void GetValue()
		{
			DataReaderDataSourceAdapter<IDataReader> adapter = new DataReaderDataSourceAdapter<IDataReader>(_reader);
			IDataReader reader = GetReaderAtRow(adapter, 1);

			object actual = adapter.GetValue(reader, new AdapterBoundField<IDataReader>("Name"));
			Assert.AreEqual("Mickey Mouse", actual);
		}

		[Test]
		public void GetValueNull()
		{
			DataReaderDataSourceAdapter<IDataReader> adapter = new DataReaderDataSourceAdapter<IDataReader>(_reader);
			IDataReader reader = GetReaderAtRow(adapter, 2);

			object actual = adapter.GetValue(reader, new AdapterBoundField<IDataReader>("Name"));
			Assert.IsNull(actual);
		}

		private IDataReader GetReaderAtRow(DataReaderDataSourceAdapter<IDataReader> adapter, int rowIndex) 
		{
			IEnumerable enumerable = adapter.GetEnumerator();
			Assert.IsNotNull(enumerable);

			IEnumerator enumerator = enumerable.GetEnumerator();
			Assert.IsNotNull(enumerator);

			for (int i = 0; i < rowIndex; i++)
			{
				enumerator.MoveNext();
			}

			return enumerator.Current as IDataReader;
		}

		private object[] GetRow(IDataReader reader)
		{
			object[] row = new object[reader.FieldCount];
			reader.GetValues(row);
			return row;
		}
	}

	internal class TestDataReader : IDataReader
	{
		public List<object[]> Rows;
		public int CurrentRow;
		public List<String> ColumnNames;

		public TestDataReader()
		{
			CurrentRow = -1;
		}

		public TestDataReader(List<string> columnNames, List<object[]> rows) : this()
		{
			ColumnNames = columnNames;
			Rows = rows;
		}
		
		public bool Read()
		{
			return CurrentRow++ < Rows.Count - 1;
		}

		public int GetValues(object[] values)
		{
			Array.Copy(Rows[CurrentRow], values, values.Length);
			return values.Length;
		}

		public int FieldCount
		{
			get { return Rows[CurrentRow].Length; }
		}

		public int GetOrdinal(string name)
		{
			return ColumnNames.IndexOf(name);
		}

		public bool IsDBNull(int i)
		{
			return Rows[CurrentRow][i] == null;
		}

		object IDataRecord.this[string name]
		{
			get { return Rows[CurrentRow][GetOrdinal(name)]; }
		}

		#region not implemented
		public DataTable GetSchemaTable()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new System.NotImplementedException();
		}

		public string GetName(int i)
		{
			throw new System.NotImplementedException();
		}

		public string GetDataTypeName(int i)
		{
			throw new System.NotImplementedException();
		}

		public Type GetFieldType(int i)
		{
			throw new System.NotImplementedException();
		}

		public object GetValue(int i)
		{
			throw new System.NotImplementedException();
		}

		public bool GetBoolean(int i)
		{
			throw new System.NotImplementedException();
		}

		public byte GetByte(int i)
		{
			throw new System.NotImplementedException();
		}

		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new System.NotImplementedException();
		}

		public char GetChar(int i)
		{
			throw new System.NotImplementedException();
		}

		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new System.NotImplementedException();
		}

		public Guid GetGuid(int i)
		{
			throw new System.NotImplementedException();
		}

		public short GetInt16(int i)
		{
			throw new System.NotImplementedException();
		}

		public int GetInt32(int i)
		{
			throw new System.NotImplementedException();
		}

		public long GetInt64(int i)
		{
			throw new System.NotImplementedException();
		}

		public float GetFloat(int i)
		{
			throw new System.NotImplementedException();
		}

		public double GetDouble(int i)
		{
			throw new System.NotImplementedException();
		}

		public string GetString(int i)
		{
			throw new System.NotImplementedException();
		}

		public decimal GetDecimal(int i)
		{
			throw new System.NotImplementedException();
		}

		public DateTime GetDateTime(int i)
		{
			throw new System.NotImplementedException();
		}

		public IDataReader GetData(int i)
		{
			throw new System.NotImplementedException();
		}


		object IDataRecord.this[int i]
		{
			get { throw new System.NotImplementedException(); }
		}

		public void Close()
		{
			throw new System.NotImplementedException();
		}

		public bool NextResult()
		{
			throw new System.NotImplementedException();
		}

		public int Depth
		{
			get { throw new System.NotImplementedException(); }
		}

		public bool IsClosed
		{
			get { throw new System.NotImplementedException(); }
		}

		public int RecordsAffected
		{
			get { throw new System.NotImplementedException(); }
		}
		#endregion
	}

}
