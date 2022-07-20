using DiffReviewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DiffReviewer.ServiceLibrary
{
    public class ImportRepository : IImportRepository
    {
        private readonly string sqlConnString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

        public int SaveOriginalDiff(string diffContent, int pullRequestNumber, DateTime createdDate)
        {
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spInsertMasterDiff", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DiffContent", SqlDbType.NVarChar).Value = diffContent;
                    cmd.Parameters.AddWithValue("@PullRequestNumber", SqlDbType.Int).Value = pullRequestNumber;
                    cmd.Parameters.AddWithValue("@CreatedDate", SqlDbType.DateTime).Value = createdDate;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            return reader.GetInt32("Id");
                        }
                    }
                }
                conn.Close();
            }
            return -1;
        }

        public int SaveIndividualDiff(int masterDiffId, string diffContent, DateTime createdDate, string originalFileName, string modifiedFileName, 
            bool isNewFile, bool isDelete, bool isBinary, bool hasHunks, string rangeStartHash, string rangeEndHash, int mode, string kind)
        {
            var retVal = -1;
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spInsertIndividualDiff", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DiffContent", SqlDbType.NVarChar).Value = diffContent;
                    cmd.Parameters.AddWithValue("@MasterDiffId", SqlDbType.NVarChar).Value = masterDiffId;
                    cmd.Parameters.AddWithValue("@CreatedDate", SqlDbType.DateTime).Value = createdDate;
                    cmd.Parameters.AddWithValue("@OriginalFileName", SqlDbType.NVarChar).Value = originalFileName;
                    cmd.Parameters.AddWithValue("@ModifiedFileName", SqlDbType.NVarChar).Value = modifiedFileName;
                    cmd.Parameters.AddWithValue("@IsNewFile", SqlDbType.Bit).Value = isNewFile;
                    cmd.Parameters.AddWithValue("@IsDelete", SqlDbType.Bit).Value = isDelete;
                    cmd.Parameters.AddWithValue("@IsBinary", SqlDbType.Bit).Value = isBinary;
                    cmd.Parameters.AddWithValue("@HasHunks", SqlDbType.Bit).Value = hasHunks;
                    cmd.Parameters.AddWithValue("@RangeStartHash", SqlDbType.NVarChar).Value = rangeStartHash;
                    cmd.Parameters.AddWithValue("@RangeEndHash", SqlDbType.NVarChar).Value = rangeEndHash;
                    cmd.Parameters.AddWithValue("@Mode", SqlDbType.Int).Value = mode;
                    cmd.Parameters.AddWithValue("@Kind", SqlDbType.NVarChar).Value = kind;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retVal =  reader.GetInt32("Id");
                        }
                    }
                }
                conn.Close();
            }
            return retVal;
        }

        public int SaveHunk(int individualDiffId, int originalRangeStartLine, int originalRangeLinesAffected,
            int newRangeStartLine, int newRangeLinesAffected, string hunkHash, string hunkText, DateTime createdDate)
        {
            var retVal = -1;
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spInsertHunk", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OriginalRangeStartLine", SqlDbType.Int).Value = originalRangeStartLine;
                    cmd.Parameters.AddWithValue("@OriginalRangeLinesAffected", SqlDbType.Int).Value = originalRangeLinesAffected;
                    cmd.Parameters.AddWithValue("@NewRangeStartLine", SqlDbType.Int).Value = newRangeStartLine;
                    cmd.Parameters.AddWithValue("@NewRangeLinesAffected", SqlDbType.Int).Value = newRangeLinesAffected;
                    cmd.Parameters.AddWithValue("@IndividualDiffId", SqlDbType.Int).Value = individualDiffId;
                    cmd.Parameters.AddWithValue("@HunkHash", SqlDbType.NVarChar).Value = hunkHash;
                    cmd.Parameters.AddWithValue("@HunkText", SqlDbType.NVarChar).Value = hunkText;
                    cmd.Parameters.AddWithValue("@CreatedDate", SqlDbType.DateTime).Value = createdDate;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retVal = reader.GetInt32("Id");
                        }
                    }
                }
                conn.Close();
            }
            return retVal;
        }

        public int SaveSnippet(int hunkId, int snippetTypeId, DateTime createdDate)
        {
            var retVal = -1;
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spInsertSnippet", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HunkId", SqlDbType.Int).Value = hunkId;
                    cmd.Parameters.AddWithValue("@SnippetTypeId", SqlDbType.Int).Value = snippetTypeId;
                    cmd.Parameters.AddWithValue("@CreatedDate", SqlDbType.DateTime).Value = createdDate;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retVal = reader.GetInt32("Id");
                        }
                    }
                }
                conn.Close();
            }
            return retVal;
        }

        public int SaveLine(int snippetId, string value, int lineTypeId, int lineOriginId, DateTime createdDate)
        {
            var retVal = -1;
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spInsertLine", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Value", SqlDbType.Int).Value = value;
                    cmd.Parameters.AddWithValue("@LineTypeId", SqlDbType.Int).Value = lineTypeId;
                    cmd.Parameters.AddWithValue("@LineOriginId", SqlDbType.Int).Value = lineOriginId;
                    cmd.Parameters.AddWithValue("@SnippetId", SqlDbType.Int).Value = snippetId;
                    cmd.Parameters.AddWithValue("@CreatedDate", SqlDbType.DateTime).Value = createdDate;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retVal = reader.GetInt32("Id");
                        }
                    }
                }
                conn.Close();
            }
            return retVal;
        }

        public int SaveLineSpan(int lineId, string value, int lineSpanTypeId, DateTime createdDate)
        {
            var retVal = -1;
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spInsertLineSpan", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Value", SqlDbType.Int).Value = value;
                    cmd.Parameters.AddWithValue("@LineSpanTypeId", SqlDbType.Int).Value = lineSpanTypeId;
                    cmd.Parameters.AddWithValue("@LineId", SqlDbType.Int).Value = lineId;
                    cmd.Parameters.AddWithValue("@CreatedDate", SqlDbType.DateTime).Value = createdDate;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            retVal = reader.GetInt32("Id");
                        }
                    }
                }
                conn.Close();
            }
            return retVal;
        }
    }
}
