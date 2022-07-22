using DiffReviewer.DTO;
using DiffReviewer.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DiffReviewer.ServiceLibrary
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string sqlConnString;

        public ReviewRepository(IConfiguration configuration)
        {
            sqlConnString = configuration.GetConnectionString("database");
        }
        public CheckInResponseDTO CheckInHunk(string hunkHash, int userId)
        {
            var retval = new CheckInResponseDTO();
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spCheckInHunk", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HunkHash", SqlDbType.NVarChar).Value = hunkHash;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = userId;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];
                        var row = dt.Rows[0];

                        retval.HunkHash = row["HunkHash"].ToString();
                        retval.IsCheckedIn = Convert.ToBoolean(row["IsCheckedIN"]);
                        retval.UserId = Convert.ToInt32(row["UserId"]);
                        retval.Status = row["Status"].ToString();
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public CheckOutResponseDTO CheckOutHunk(string hunkHash, int userId)
        {
            var retval = new CheckOutResponseDTO();
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spCheckOutHunk", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HunkHash", SqlDbType.NVarChar).Value = hunkHash;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = userId;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];
                        var row = dt.Rows[0];

                        retval.HunkHash = row["HunkHash"].ToString();
                        retval.IsCheckedOutByOtherUser = Convert.ToBoolean(row["IsCheckedOutByOtherUser"]);
                        retval.UserId = Convert.ToInt32(row["CheckOutUserId"]);
                        retval.Status = row["Status"].ToString();             
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public CheckOutStatusResponseDTO GetCheckOutStatus(string hunkHash)
        {
            var retval = new CheckOutStatusResponseDTO();
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetHunkCheckOutStatus", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HunkHash", SqlDbType.NVarChar).Value = hunkHash;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];
                        var row = dt.Rows[0];

                        retval.HunkHash = row["HunkHash"].ToString();
                        retval.CheckOutUserId = Convert.ToInt32(row["CheckOutUserId"]);
                        retval.CheckOutDate = !row.IsNull("CheckOutUserId") ? Convert.ToDateTime(row["CheckOutUserId"]) : (DateTime?)null;
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public HunkReviewsDTO GetHunksToReview(int pullRequestNumber, bool showAll, bool showFullyReviewed, bool showFullyApproved)
        {
            var retval = new HunkReviewsDTO();
            retval.PullRequestNumber = pullRequestNumber;
            retval.Hunks = new List<HunkReviewDTO>();

            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetCurrentChangedHunks", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PullRequestNumber", SqlDbType.Int).Value = pullRequestNumber;
                    cmd.Parameters.AddWithValue("@ShowAll", SqlDbType.Bit).Value = showAll;
                    cmd.Parameters.AddWithValue("@ShowFullyReviewed", SqlDbType.Bit).Value = showFullyReviewed;
                    cmd.Parameters.AddWithValue("@ShowFullyApproved", SqlDbType.Bit).Value = showFullyApproved;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];
                        
                        foreach(DataRow row in dt.Rows)
                        {
                            var dto = new HunkReviewDTO();
                            dto.MasterDiffId = Convert.ToInt32(row["MasterDiffId"]);
                            dto.IndividualDiffId = Convert.ToInt32(row["IndividualDiffId"]);
                            dto.OriginalFileName = row["OriginalFileName"].ToString();
                            dto.ModifiedFileName = row["ModifiedFileName"].ToString();
                            dto.HunkId = Convert.ToInt32(row["HunkId"]);
                            dto.HunkHash = row["HunkHash"].ToString();
                            dto.HunkText = row["HunkText"].ToString();
                            dto.IsCheckedOut = Convert.ToBoolean(row["IsCheckedOut"]);
                            dto.AcceptedCount = Convert.ToInt32(row["AcceptedCount"]);
                            dto.ReviewCount = Convert.ToInt32(row["ReviewCount"]);
                            dto.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                            dto.ApprovalCount = Convert.ToInt32(row["ApprovalCount"]);
                            dto.IsFullyReviewed = Convert.ToBoolean(row["IsFullyReviewed"]);
                            dto.IsFullyApproved = Convert.ToBoolean(row["IsFullyApproved"]);

                            retval.Hunks.Add(dto);
                        }
                    }
                }
                conn.Close();
            }
            return retval;
        }
    
        public HunkReviewResponseDTO SetHunkReviewStatus(string hunkHash, bool isAccepted, int userId, string comments)
        {
            var retval = new HunkReviewResponseDTO();
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spReviewHunk", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HunkHash", SqlDbType.NVarChar).Value = hunkHash;
                    cmd.Parameters.AddWithValue("@IsAccepted", SqlDbType.Bit).Value = isAccepted;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.AddWithValue("@Comments", SqlDbType.NVarChar).Value = comments;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];
                        var row = dt.Rows[0];

                        retval.HunkHash = row["HunkId"].ToString();
                        retval.IsAccepted = Convert.ToBoolean(row["IsAccepted"]);
                        retval.UserId = Convert.ToInt32(row["UserId"]);
                        retval.Status = row["Status"].ToString();
                    }
                }
                conn.Close();
            }
            return retval;
        }
        public HunkApprovalResponseDTO SetHunkApprovalStatus(string hunkHash, bool isApproved, int userId, string comments)
        {
            var retval = new HunkApprovalResponseDTO();
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spApproveHunk", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HunkHash", SqlDbType.NVarChar).Value = hunkHash;
                    cmd.Parameters.AddWithValue("@isApproved", SqlDbType.Bit).Value = isApproved;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = userId;
                    cmd.Parameters.AddWithValue("@Comments", SqlDbType.NVarChar).Value = comments;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];
                        var row = dt.Rows[0];

                        retval.HunkHash = row["HunkHash"].ToString();
                        retval.IsApproved = Convert.ToBoolean(row["IsApproved"]);
                        retval.UserId = Convert.ToInt32(row["UserId"]);
                        retval.Status = row["Status"].ToString();
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public List<int> GetPullRequestNumbers()
        {
            var retval = new List<int>();

            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetPullRequestNumbers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];

                        foreach (DataRow row in dt.Rows)
                        {
                            if (!row.IsNull("PullRequestNumber"))
                            {
                                var prNumber = Convert.ToInt32(row["PullRequestNumber"]);
                                retval.Add(prNumber);
                            }
                        }
                    }
                }
                conn.Close();
            }
            return retval;
        }

        public ReviewStatsDTO GetReviewStats(int pullRequestNumber)
        {
            var retval = new ReviewStatsDTO();
            using (var conn = new SqlConnection(sqlConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("spGetReviewStats", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PullRequestNumber", SqlDbType.Int).Value = pullRequestNumber;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;

                        var ds = new DataSet();
                        da.Fill(ds, "results");

                        var dt = ds.Tables["results"];
                        var row = dt.Rows[0];

                        retval.TotalHunksCheckedOut = Convert.ToInt32(row["TotalHunksCheckedOut"]);
                        retval.TotalAcceptedReviews = Convert.ToInt32(row["TotalAcceptedReviews"]);
                        retval.TotalRejectedReviews = Convert.ToInt32(row["TotalRejectedReviews"]);
                        retval.TotalReviews = Convert.ToInt32(row["TotalReviews"]);
                        retval.TotalFullyReviewed = Convert.ToInt32(row["TotalFullyReviewed"]);
                        retval.TotalReviewsApproved = Convert.ToInt32(row["TotalReviewsApproved"]);
                        retval.TotalReviewsNotApproved = Convert.ToInt32(row["TotalReviewsNotApproved"]);
                        retval.TotalApprovals = Convert.ToInt32(row["TotalApprovals"]);
                        retval.TotalFullyApproved = Convert.ToInt32(row["TotalFullyApproved"]);
                        retval.HunkCount = Convert.ToInt32(row["HunkCount"]);
                    }
                }
                conn.Close();
            }
            return retval;
        }
    }
}
