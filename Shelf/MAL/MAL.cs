using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shelf.Json;
using Shelf.Entity;
using Shelf.Functions;
using JerloPH_CSharp;

namespace Shelf
{
    public static class MAL
    {
        public static string XmlMedia(string media, Entry item)
        {
            if (media.ToLower().Equals("anime"))
                return XmlAnime(item);
            else
                return XmlManga(item);
        }
        public static string XmlAnime(Entry item)
        {
            string media = "anime";
            string xmltoWrite = Media(media);
            xmltoWrite += Val(item.Media.IdMal, "series_animedb_id");
            xmltoWrite += Text(item.Media.Title.Romaji, "series_title");
            xmltoWrite += Val(Format(item.Media.Format), "series_type");
            xmltoWrite += Val(item.Media.Episodes, "series_episodes");
            //xmltoWrite += Val("0", "my_id");
            xmltoWrite += Val(item.Progress, "my_watched_episodes");
            xmltoWrite += DateText(item.StartedAt, "my_start_date");
            xmltoWrite += DateText(item.CompletedAt, "my_finish_date");
            //xmltoWrite += Val("", "my_rated");
            xmltoWrite += Val(item.Score, "my_score");
            //xmltoWrite += Val("", "my_storage");
            xmltoWrite += Val(Status(item.Status, media), "my_status");
            xmltoWrite += Text(item.Notes, "my_comments");
            //xmltoWrite += Val("0", "my_times_watched");
            //xmltoWrite += Val("", "my_rewatch_value");
            //xmltoWrite += Text("", "my_tags");
            xmltoWrite += Val((item.Status.Equals("REPEATING") ? "1" : "0"), "my_rewatching");
            if (item.Status.Equals("REPEATING"))
            {
                xmltoWrite += Val(item.Progress, "my_rewatching_ep");
            }
            else
                xmltoWrite += Val("0", "my_rewatching_ep");

            xmltoWrite += Val("1", "update_on_import");
            xmltoWrite += Media(media, true);
            return xmltoWrite;
        }
        public static string XmlManga(Entry item)
        {
            string media = "manga";
            string xmltoWrite = Media(media);
            xmltoWrite += Val(item.Media.IdMal, "manga_mangadb_id");
            xmltoWrite += Text(item.Media.Title.Romaji, "manga_title");
            xmltoWrite += Val(item.Media.Volumes, "manga_volumes");
            xmltoWrite += Val(item.Media.Chapters, "manga_chapters");
            xmltoWrite += Val(item.ProgressVolumes, "my_read_volumes");
            xmltoWrite += Val(item.Progress, "my_read_chapters");
            xmltoWrite += DateText(item.StartedAt, "my_start_date");
            xmltoWrite += DateText(item.CompletedAt, "my_finish_date");
            xmltoWrite += Val(item.Score, "my_score");
            xmltoWrite += Val(Status(item.Status, media), "my_status");
            xmltoWrite += Text(item.Notes, "my_comments");
            //xmltoWrite += Val("0", "my_times_read");
            //xmltoWrite += Text("", "my_tags");
            //xmltoWrite += Val("", "my_reread_value");
            xmltoWrite += Val((item.Status.Equals("REPEATING") ? "YES" : "NO"), "my_rereading");
            //xmltoWrite += Val("YES", "my_discuss");
            //xmltoWrite += Val("default", "my_sns");
            xmltoWrite += Val("1", "update_on_import");
            xmltoWrite += Media(media, true);
            return xmltoWrite;
        }
        public static string Media(string media, bool closing = false)
        {
            if (!closing)
                return $"\t<{media.Trim().ToLower()}>\n";

            return $"\t</{media.Trim().ToLower()}>\n";
        }
        public static string Val(string content, string name)
        {
            string log = ValidateStr(content);
            return $"\t\t<{name}>{log}</{name}>\n";
        }
        public static string Val(long value, string name)
        {
            return $"\t\t<{name}></{name}>\n";
        }
        public static string Val(long? value, string name)
        {
            if (value != null)
                return $"\t\t<{name}>{ValidateStr(value.ToString())}</{name}>\n";

            return $"\t\t<{name}></{name}>\n";
        }
        public static string Text(string content, string name)
        {
            string log = ValidateStr(content);
            return $"\t\t<{name}><![CDATA[{log}]]></{name}>\n";
        }
        public static string DateText(EntryDate date, string name)
        {
            if (date != null)
            {
                string year = (date.Year != null) ? date.Year.ToString() : "0000";
                string month = (date.Month != null) ? date.Month.ToString() : "00";
                if (month.Length == 1)
                    month = $"0{month}";

                string day = (date.Day != null) ? date.Day.ToString() : "00";
                if (day.Length == 1)
                    day = $"0{day}";

                try
                {
                    return Val($"{year}-{month}-{day}", name);
                }
                catch (Exception ex) { Logs.Err(ex); }
            }
            return Val("0000-00-00", name);
        }
        public static string Status(string status, string mediatype)
        {
            string AnilistStatus = ValidateStr(status);
            string media = mediatype.ToLower();
            return AnilistStatus switch
            {
                "COMPLETED" => "Completed",
                "PAUSED" => "On-Hold",
                "CURRENT" => media.Equals("anime") ? "Watching" : "Reading",
                "DROPPED" => "Dropped",
                "PLANNING" => media.Equals("anime") ? "Plan to Watch" : "Plan to Read",
                "REPEATING" => media.Equals("anime") ? "Watching" : "Reading",
                _ => ""
            };
        }
        public static string Format(string format)
        {
            string AnilistStatus = ValidateStr(format);
            return AnilistStatus switch
            {
                "TV" => "TV",
                "TV_SHORT" => "TV",
                "MOVIE" => "Movie",
                "SPECIAL" => "Special",
                "OVA" => "OVA",
                "ONA" => "ONA",
                "MUSIC" => "Music",
                "MANGA" => "Manga",
                "NOVEL" => "Light Novel",
                "ONE_SHOT" => "One-shot",
                _ => ""
            };
        }
        public static string PrependInfo(string media, string username, StatusCount count)
        {
            string mediastring = media.ToLower();
            string malprepend = $"<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n";
            string mediawatchread = mediastring.Equals("anime") ? "watch" : "read";
            malprepend += "<!-- \n\t Exported File Generated by 'Shelf', https://github.com/JerloPH/Shelf \n\t";
            malprepend += $" Version {Functions.GlobalFunc.GetAppVersion()} \n--> \n";
            malprepend += $"<my{mediastring}list>\n";
            malprepend += "\t<myinfo>\n";
            malprepend += Val("", "user_id");
            malprepend += Val(username, "user_name");
            malprepend += Val((mediastring.Equals("anime") ? "1" : "2"), "user_export_type");
            // Counts
            malprepend += Val(count.Total.ToString(), $"user_total_{mediastring}");
            malprepend += Val(count.Current.ToString(), $"user_total_{mediawatchread}ing");
            malprepend += Val(count.Complete.ToString(), $"user_total_completed");
            malprepend += Val(count.Onhold.ToString(), $"user_total_onhold");
            malprepend += Val(count.Drop.ToString(), $"user_total_dropped");
            malprepend += Val(count.Plan.ToString(), $"user_total_planto{mediawatchread}");
            // Finalize
            malprepend += "\t</myinfo>\n";
            return malprepend;
        }
        public static string ValidateStr(string text)
        {
            if (!String.IsNullOrWhiteSpace(text))
                return text;

            return "";
        }
    }
}
