using Medicaly.mails;
using Medicaly.Models;
using Medicaly.Repositories;
using Medicaly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Services
{
    public static class TransactionService
    {
        public static string updateStatus(int id, int status, string kurir, string trackId)
        {
            if (kurir != null && trackId != null)
            {
                if (TransactionRepository.updateStatusShipment(id, status, kurir, trackId))
                {
                    DetailTransaction detailTransaction = TransactionRepository.getDetailById(id);
                    sendEmail(TransactionRepository.getHeaderTransactionbyId(convertNullable(detailTransaction.TransactionId)));
                    return "Success update status!";
                }
            }
            else
            {
                if (TransactionRepository.updateStatus(id, status))
                {
                    return "Success update status!";
                }
            }

            return "Cannot update status!";
        }

        public static TransactionViewModel getAllTransaction(int id)
        {
            List<DetailTransaction> detailTransactions = TransactionRepository.getDetailTransactionByTrId(id);
            TransactionViewModel transactionView = new TransactionViewModel();

            transactionView.detailTransactions = detailTransactions;
            transactionView.header = TransactionRepository.getHeaderTransactionbyId(id);

            return transactionView;
        }

        public static List<DetailTransaction> getTransactions(string id)
        {
            return TransactionRepository.GetDetailByApotekId(int.Parse(id));
        }

        public static List<DetailTransaction> getAdminAllTransactions()
        {
            return TransactionRepository.getDetails();
        }


        public static List<DetailTransaction> getUserTransactions(string id)
        {
            return TransactionRepository.GetDetailByCustomerId(int.Parse(id));
        }

        private static int convertNullable(int? number)
        {
            int newNumber = 0;
            if (number == null)
                newNumber = 0;
            else
                newNumber = number.Value;

            return newNumber;
        }

        private static void sendEmail(HeaderTransaction headerTransaction)
        {
            GMailer mailer = new GMailer();
            mailer.ToEmail = headerTransaction.Alamat.Customer.Email;
            mailer.Subject = "Medicaly - Order: " + headerTransaction.Id;
            mailer.Body =
                "<html>" +
                    "<body style='background-color:#e2e1e0;font-family: Open Sans, sans-serif;font-size:100%;font-weight:400;line-height:1.4;color:#000;'>" +
                        "<table style= 'max-width:670px;margin:50px auto 10px;background-color:#fff;padding:50px;-webkit-border-radius:3px;-moz-border-radius:3px;border-radius:3px;-webkit-box-shadow:0 1px 3px rgba(0,0,0,.12),0 1px 2px rgba(0,0,0,.24);-moz-box-shadow:0 1px 3px rgba(0,0,0,.12),0 1px 2px rgba(0,0,0,.24);box-shadow:0 1px 3px rgba(0,0,0,.12),0 1px 2px rgba(0,0,0,.24); border-top: solid 10px green;'>" +
                            "<thead style='margin-top: 20px;'>" +
                                "<tr>" +
                                    "<th style='text-align:left;'>Medicaly</th>" +
                                    "<th style='text-align:right;font-weight:400;'>" + DateTime.Now + "</th>" +
                                "</tr>" +
                            "</thead>" +
                            "<tbody>" +
                                "<tr>" +
                                    "<td style='height:35px;'></td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan='2' style='border: solid 1px #ddd; padding:10px 20px;'>" +
                                    "<p style='font-size:14px;margin:0 0 6px 0;'><span style='font-weight:bold;display:inline-block;min-width:150px'>Order status</span><b style='color:green;font-weight:normal;margin:0'>PAID - DIKIRIM</b></p>" +
                                    "<p style='font-size:14px;margin:0 0 6px 0;'><span style='font-weight:bold;display:inline-block;min-width:146px'>Transaction ID</span> " + headerTransaction.Id + "</p>" +
                                    "<p style='font-size:14px;margin:0 0 0 0;'><span style='font-weight:bold;display:inline-block;min-width:146px'>Order amount</span> " + Convert.ToDecimal(headerTransaction.TotalHarga).ToString("C2") + "</p></td></tr><tr><td style='height:35px;'></td></tr><tr>" +
                                        "<td style='width:50%;padding:20px;vertical-align:top'>" +
                                          "<p style='margin:0 0 10px 0;padding:0;font-size:14px;'><span style='display:block;font-weight:bold;font-size:13px'>Name</span> " + headerTransaction.Alamat.NamaPenerima + "</p>" +
                                          "<p style='margin:0 0 10px 0;padding:0;font-size:14px;'><span style='display:block;font-weight:bold;font-size:13px;'>Email</span> " + headerTransaction.Alamat.Customer.Email + "</p>" +
                                          "<p style='margin:0 0 10px 0;padding:0;font-size:14px;'><span style='display:block;font-weight:bold;font-size:13px;'>Phone</span> " + headerTransaction.Alamat.Customer.NoHandphone + "</p>" +
                                        "</td>" +
                                        "<td style='width:50%;padding:20px;vertical-align:top'>" +
                                          "<p style='margin:0 0 10px 0;padding:0;font-size:14px;'><span style='display:block;font-weight:bold;font-size:13px;'>Address</span> " + headerTransaction.Alamat.Alamat1 + "</p>" +
                                          "<p style='margin:0 0 10px 0;padding:0;font-size:14px;'><span style='display:block;font-weight:bold;font-size:13px;'>Kota</span> " + headerTransaction.Alamat.KotaKecamatan + "</p>" +
                                          "<p style='margin:0 0 10px 0;padding:0;font-size:14px;'><span style='display:block;font-weight:bold;font-size:13px;'>Kota</span> " + headerTransaction.Alamat.KodePost + "</p>" +
                                        "</td>" +
                                      "</tr>" +
                                      "<tr>" +
                                        "<td colspan='2' style='font-size:20px;padding:30px 15px 0 15px;'>Items</td>" +
                                      "</tr>" +
                                      "<tr>" +
                                        "<td colspan='2' style='padding:15px;'>" +
                                            orderString(headerTransaction.Id) +
                                        "</td>" +
                                      "</tr>" +
                                    "</tbody>" +
                                    "<tfooter style='margin-bottom: 20px;'>" +
                                      "<tr>" +
                                        "<td colspan='2' style='font-size:14px;padding:50px 15px 0 15px;'>" +
                                          "<strong style='display:block;margin:0 0 10px 0;'>Regards</strong> Medicaly<br> New York, Avenue Street 10<br><br>" +
                                          "<b>Phone:</b> 03552-222011<br>" +
                                          "<b>Email:</b> contact@medicaly.in" +
                                        "</td>" +
                                      "</tr>" +
                                    "</tfooter>" +
                                  "</table>" +
                                "</body>" +
                                "</html>";
            mailer.IsHtml = true;
            mailer.Send();
        }

        private static string orderString(int id)
        {
            string order = "";

            List<DetailTransaction> detailTransaction = TransactionRepository.getDetailTransactionByTrId(id);
            foreach (var item in detailTransaction)
            {
                if (item.Kurir != null && item.TrackingId != null)
                {
                    order += "<p style='font-size:14px;margin:0;padding:10px;border:solid 1px #ddd;font-weight:bold;'><span style='display:block;font-size:13px;font-weight:normal;'>" + item.Product.Nama + "</span> " + Convert.ToDecimal(item.Product.Price).ToString("C2") + " <b style='font-size:12px;font-weight:300;'> " + item.Product.Pharmacy.NamaPharmacy + " Qty: " + item.Quantity + " Tipe: " + item.Product.Type + "</b>" +
                    "<p>Kurir: " + item.Kurir + "</p><p> Tracking ID: " + item.TrackingId + " </p></p>";
                } else
                {
                    order += "<p style='font-size:14px;margin:0;padding:10px;border:solid 1px #ddd;font-weight:bold;'><span style='display:block;font-size:13px;font-weight:normal;'>" + item.Product.Nama + "</span> " + Convert.ToDecimal(item.Product.Price).ToString("C2") + " <b style='font-size:12px;font-weight:300;'> " + item.Product.Pharmacy.NamaPharmacy + " Qty: " + item.Quantity + " Tipe: " + item.Product.Type + "</b>" +
                    "<p>Kurir: Waiting</p><p> Tracking ID: Waiting </p></p>";
                }
            }

            return order;
        }

    }
}