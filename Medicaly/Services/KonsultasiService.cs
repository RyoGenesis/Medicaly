using Medicaly.mails;
using Medicaly.Models;
using Medicaly.Repositories;
using Medicaly.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Medicaly.Services
{
    public static class KonsultasiService
    {
        public static KonsultasiViewModel getKonsultasiView(int spesialisId)
        {
            List<Konsultasi> konsultasis = KonsultasiRepository.getKonsultasiBySpesialis(spesialisId) ;
            KonsultasiViewModel konsultasiView = new KonsultasiViewModel();

            konsultasiView.konsultasi = konsultasis;

            return konsultasiView;
        }

        public static bool addKonsultasi(Konsultasi konsultasi, string path)
        {
            if (!path.Equals(""))
            {
                string fileName = Path.GetFileNameWithoutExtension(konsultasi.ImageUpload.FileName);
                string extension = Path.GetExtension(konsultasi.ImageUpload.FileName);
                fileName = konsultasi.Nama + "_" + konsultasi.SpesialisId + "_" + fileName + extension;
                konsultasi.FilePendukung = fileName;
                konsultasi.ImageUpload.SaveAs(Path.Combine(path, fileName));
            }

            // Default not answered
            konsultasi.Status = 0;

            if (KonsultasiRepository.addKonsultasi(konsultasi))
            {
                return true;
            }

            return false;
        }

        public static Konsultasi getKonsultasiById(int id)
        {
            if (id.ToString() != null)
            {
                return KonsultasiRepository.getKonsultasiById(id);
            }

            return null;
        }

        public static bool editKonsultasi(Konsultasi konsultasi)
        {
            if (KonsultasiRepository.getKonsultasiById(konsultasi.Id) != null)
            {
                Konsultasi oldKonsultasi = KonsultasiRepository.getKonsultasiById(konsultasi.Id);

                oldKonsultasi.Jawaban = konsultasi.Jawaban;
                oldKonsultasi.Status = 1;
                oldKonsultasi.DoktorId = konsultasi.DoktorId;
                oldKonsultasi.Doctor = DoctorRepository.getDoctorById(konsultasi.DoktorId);
                sendEmail(oldKonsultasi);

                return KonsultasiRepository.updateKonsultasi(oldKonsultasi);
            }

            return false;
        }

        private static void sendEmail(Konsultasi konsultasi)
        {
            GMailer mailer = new GMailer();
            mailer.ToEmail = konsultasi.Email;
            mailer.Subject = "Konsultasi - " + konsultasi.Nama;
            mailer.Body =
                  "<html>" +
                    "<body style='background-color:#e2e1e0;font-family: Open Sans, sans-serif;font-size:100%;font-weight:400;line-height:1.4;color:#000;'>" +
                        "<table style= 'max-width:670px;margin:50px auto 10px;background-color:#fff;padding:50px;-webkit-border-radius:3px;-moz-border-radius:3px;border-radius:3px;-webkit-box-shadow:0 1px 3px rgba(0,0,0,.12),0 1px 2px rgba(0,0,0,.24);-moz-box-shadow:0 1px 3px rgba(0,0,0,.12),0 1px 2px rgba(0,0,0,.24);box-shadow:0 1px 3px rgba(0,0,0,.12),0 1px 2px rgba(0,0,0,.24); border-top: solid 10px green;'>" +
                            "<thead style='margin-top: 20px;'>" +
                                "<tr>" +
                                    "<th style='text-align:left;'>Medicaly - Konsultasi</th>" +
                                    "<th style='text-align:right;font-weight:400;'>" + DateTime.Now + "</th>" +
                                "</tr>" +
                            "</thead>" +
                            "<tbody>" +
                                  "<tr>" +
                                    "<td style='height:35px;'></td>" +
                                  "</tr>" +
                                  "<tr>" +
                                    "<td colspan='2' style='border: solid 1px #ddd; padding:10px 20px;'>" +
                                    "<p style='font-size:14px;margin:0 0 6px 0;'><span style='font-weight:bold;display:inline-block;min-width:150px'> Halo, " + konsultasi.Nama  + " terima kasih sudah menggunakan fitur konsultasi kami. Berikut jawaban dari dokter kami.</p>" +
                                    "</td>" +
                                  "</tr>" +
                                "<tr>" +
                                    "<td style='height:35px;'></td>" +
                                "</tr>" +
                                        "<td style='width:50%;padding:20px;vertical-align:top'>" +
                                          "<p style='margin:0 0 10px 0;padding:0;font-size:14px;'><span style='display:block;font-weight:bold;font-size:13px'>Name</span> " + konsultasi.Nama + "</p>" +
                                          "<p style='margin:0 0 10px 0;padding:0;font-size:14px;'><span style='display:block;font-weight:bold;font-size:13px;'>Email</span> " + konsultasi.Email + "</p>" +
                                        "</td>" +
                                        "<td style='width:50%;padding:20px;vertical-align:top'>" +
                                          "<p style='margin:0 0 10px 0;padding:0;font-size:14px;'><span style='display:block;font-weight:bold;font-size:13px;'>Nama Doktor</span> " + konsultasi.Doctor.Nama + "</p>" +
                                          "<p style='margin:0 0 10px 0;padding:0;font-size:14px;'><span style='display:block;font-weight:bold;font-size:13px;'>STR</span> " + konsultasi.Doctor.STR + "</p>" +
                                          "<p style='margin:0 0 10px 0;padding:0;font-size:14px;'><span style='display:block;font-weight:bold;font-size:13px;'>Spesialis</span> " + konsultasi.Doctor.Spesiali.Nama + "</p>" +
                                          "<p style='margin:0 0 10px 0;padding:0;font-size:14px;'><span style='display:block;font-weight:bold;font-size:13px;'>Email</span> " + konsultasi.Doctor.Email  + "</p>" +
                                        "</td>" +
                                      "</tr>" +
                                      "<tr>" +
                                        "<td colspan='2' style='font-size:20px;padding:30px 15px 0 15px;'>Pertanyaan</td>" +
                                      "</tr>" +
                                      "<tr>" +
                                        "<td colspan='2' style='padding:15px;'>" +
                                            "<textarea style = 'margin-top: 10px;margin-left: 0px;width: 500px;height: 100px;-moz-border-bottom-colors: none;-moz-border-left-colors: none;-moz-border-right-colors: none;-moz-border-top-colors: none;background: none repeat scroll 0 0 rgba(0, 0, 0, 0.07);border-color: -moz-use-text-color #FFFFFF #FFFFFF -moz-use-text-color;border-image: none;border-radius: 6px 6px 6px 6px;border-style: none solid solid none;border-width: medium 1px 1px medium;box-shadow: 0 1px 2px rgba(0, 0, 0, 0.12) inset;color: #555555;font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif;font-size: 1em;line-height: 1.4em;padding: 5px 8px;transition: background-color 0.2s ease 0s;'  rows = '20' name = 'comment[text]' id = 'comment_text' cols = '80' class='ui-autocomplete-input' autocomplete='off' role='textbox' aria-autocomplete='list' aria-haspopup='true' readonly>" +
                                            konsultasi.Detail +
                                            "</textarea>" +
                                        "</td>" +
                                      "</tr>" +
                                      "<tr>" +
                                        "<td colspan='2' style='font-size:20px;padding:30px 15px 0 15px;'>Jawaban Dokter</td>" +
                                      "</tr>" +
                                      "<tr>" +
                                        "<td colspan='2' style='padding:15px;'>" +
                                            "<textarea style = 'margin-top: 10px;margin-left: 0px;width: 500px;height: 100px;-moz-border-bottom-colors: none;-moz-border-left-colors: none;-moz-border-right-colors: none;-moz-border-top-colors: none;background: none repeat scroll 0 0 rgba(0, 0, 0, 0.07);border-color: -moz-use-text-color #FFFFFF #FFFFFF -moz-use-text-color;border-image: none;border-radius: 6px 6px 6px 6px;border-style: none solid solid none;border-width: medium 1px 1px medium;box-shadow: 0 1px 2px rgba(0, 0, 0, 0.12) inset;color: #555555;font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif;font-size: 1em;line-height: 1.4em;padding: 5px 8px;transition: background-color 0.2s ease 0s;'  rows = '20' name = 'comment[text]' id = 'comment_text' cols = '80' class='ui-autocomplete-input' autocomplete='off' role='textbox' aria-autocomplete='list' aria-haspopup='true' readonly>" +
                                            konsultasi.Jawaban +
                                            "</textarea>" +
                                        "</td>" +
                                      "</tr>" +
                                    "</tbody>" +
                                    "<tfooter style='margin-bottom: 20px;'>" +
                                      "<tr>" +
                                        "<td colspan='2' style='font-size:14px;padding:50px 15px 0 15px;'>" +
                                          "<strong style='display:block;margin:0 0 10px 0;'>Regards</strong> Medicaly<br> New York, Avenue Street 10<br><br>" +
                                          "<b>Phone:</b> 03552-222011<br>" +
                                          "<b>Email:</b> pr.medicaly@gmail.com" +
                                          "<br><a href='https://localhost:44378/'>Medicaly</a>" +
                                        "</td>" +
                                      "</tr>" +
                                    "</tfooter>" +
                                  "</table>" +
                                "</body>" +
                                "</html>";
            mailer.IsHtml = true;
            mailer.Send();
        }
    }
}