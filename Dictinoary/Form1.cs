using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Dictinoary
{
struct date
    {
        public string year;
        public string month;
        public string day;
    }
    struct name
    {
        public string xing;
        public string ming;
    }
    public partial class Form1 : Form
    {
        string FileName;
        public static int THRESHOLD = 6;//字典生成的最短密码长度
        const int peopleCount = 4;
        name[] names = new name[peopleCount];
        date[] birthdays = new date[peopleCount];
        string[] phones = new string[peopleCount];
        string[] qqs = new string[peopleCount];
        string identity = "";
        string city = "";
        string biyexuexiao = "";
        string changyongID = "";
        string shegongku = "";
        string otherinfo = "";
        bool include_top_300 ;//生成的字典含有top300
        bool use_big_shuzi ;//不使用大数字数组，使用shuzi_mini[]
        //top300密码,直接就是密码，不需要拼接
        string[] top300 = { "123456789", "111111", "5201314", "123123", "000000", "a123456", "7758521", "123456a", "1314520", "123321", "hao123456", "qq123456", "12345678", "woaini1314",
            "666666", "5211314", "qazwsx", "woaini", "seoer2010", "7758258", "31415926", "520520", "100200", "a123456789", "1234567", "1q2w3e4r", "123qwe", "1qaz2wsx", "888888", "200884",
            "112233", "584520", "qwe123", "1234567890", "123654", "110110", "201314", "asd123", "654321", "456852", "25257758", "abc123", "1314521", "11111111", "aa123456", "147258369",
            "88888888", "123123123", "123456789a", "woaini123", "5845211314", "a123123", "110120", "qwerty", "iloveyou", "123456abc", "q123456", "7758520", "123456qq", "521521", "123698745",
            "windows", "asd123456", "woaiwojia", "qwe123456", "123456aa", "121212", "z123456", "abc123456", "woaini520", "211314", "zxcvbnm", "147258", "password", "778899", "159357", "qazqaz",
            "101010", "131421", "3344520", "123000", "woaini521", "3.1415926", "qazwsxedc", "as123456", "a5201314", "buzhidao", "789789", "147852", "584521", "aini1314", "abcd1234", "00000000", "158158", "5845201314", "110119", "xiaoxiao", "yangyang", "woainima", "qqqqqq", "753951", "721521", "987654321", "584131421", "3344521", "168168", "w123456", "asdasd", "aaaaaa", "qweqwe", "999999", "asdfasdf", "159753", "222222", "251314", "7788521", "5841314520", "wocaonima", "zxc123", "131420", "asdfgh", "zxcvbnm123", "zhang123", "1q2w3e", "woshishui", "1234qwer", "369369", "584131420", "11223344", "5201314520", "qwer1234", "789456123", "12301230", "951753", "520530", "888999", "314159", "963852", "789456", "123456q", "q1w2e3r4", "123abc", "1111111", "asdasdasd", "336699", "456123", "qweasd", "qweasdzxc", "1q2w3e4r5t", "qwe300100", "25251325", "709394", "521125", "010203", "11112222", "zhangwei", "7895123", "caonima", "007007", "258258", "333333", "120120", "5213344", "1234abcd", "147896325", "nicholas", "1123581321", "518518", "qaz123", "555555", "a111111", "111222", "110110110", "7777777", "1qazxsw2", "12344321", "135790", "110120119", "qq123123", "12369874", "a1314520", "5201314a", "134679", "741852963", "worinima", "12341234", "3415413", "wangjian", "asdqwe22", "qq5201314", "5203344", "321321", "wojiushiwo", "zxc123456", "zhanglei", "520123", "135792468", "a000000", "852456", "77585210", "asdzxc", "112358", "wan0425", "1357924680", "1230123", "135246", "zhangjian", "142536", "123456123", "a12345", "wang123", "123qweasd", "521314", "qazwsx123", "666888", "123456asd", "119119", "shmily", "584201314", "111111a", "qw123456", "123123qq", "321654", "woailaopo", "121314", "102030", "asdf1234", "qq123456789", "wangyang", "a123456b", "111222333", "12qwaszx", "qwertyuiop", "123456aaa", "235689", "123520", "111000", "124578", "123457", "123123a", "a123456a", "lgx910918", "zxczxc", "520520520", "woaiwoziji", "yj123456", "aaa123456", "qaz123456", "212121", "operation", "564335", "a1b2c3d4", "520025", "123asd", "weiwei", "wodemima", "xiaofeng", "100200300", "123654789", "jiajia", "19841020", "xiaolong", "2597758", "aptx4869", "nihaoma", "19861010", "mymedia", "zxcvbn", "123789", "tianshi", "232323", "asdfghjkl", "123321123", "aaa123", "123qwe123", "16897168", "521521521", "110120130", "woshishei", "74108520", "a1234567", "2587758", "abcd123", "741852", "456456456", "1230456", "19861020", "123456789q", "0000000", "555666", "mmgg168168", "1111111111", "ab12345", "12121212", "258369", "l123456", "5841314521", "369258147", "hao123", "ww111111" };
        //库里的常见键盘模式，作为密码的一个组成部分
        string[] keyboard = { "qwer", "qq123456", "zxcvbnm", "1qaz", "q123456", "2wsx", "1q2w3e4r",
            "assw", "q123", "qwerty",  "zxcv", "qq123", "qwert", "aaaa", "wasd", "asdfgh", "aaaaaa",
            "qqqqqq", "qwe3", "1q2w3e", "asdfghjkl", "asdfg", "qwertyuiop", "qwaszx", "1q2w3e4r5t",
            "1qazxsw2", "123w", "zxcvbn", "zxcvb", "wawa", "jiji", "qq123456789", "12qwaszx" };
        // string[] pinyin = { "li", "woaini", "le", "wang", "wo", "zhang", "wa", "ma", "sa", "yu", "liu", "se", "da", "de", "yang", "you", "an", "chen", "ba", "ai", "la", "te", "he", "re", "me", "hao", "lin", "ni", "ne", "mi", "ha", "ca", "za", "bo", "wei", "sun", "wan", "ke", "ri", "woai", "su", "lu", "long", "ta", "po", "huang", "hu", "xiao", "wu", "mo", "pa", "yan", "ye", "ce", "feng", "fa", "si", "zhou", "xu", "di", "min", "ya", "zhao", "aini", "zhu", "shi", "bi", "en", "ji", "han", "na", "ge", "wen", "fo", "ang", "jiang", "ga", "jing", "woshi", "fu", "ti", "liang", "jia", "yuan", "jun", "man", "jie", "jin", "lan", "jian", "yi", "gao", "xin", "hua", "ka", "ming", "cheng", "ling", "fei", "tang", "dan", "fan", "hui", "lei", "qi", "ju", "luo", "song", "nan", "guo", "dong", "du", "bin", "ren", "ying", "she", "ci", "tao", "gu", "meng", "zheng", "fang", "qu", "peng", "cai", "ze", "sha", "tu", "pan", "weiwei", "woaiwojia", "shen", "pi", "xiang", "xue", "yun", "tian", "xia", "nihao", "caonima", "yue", "cao", "xie", "bai", "mu", "hai", "tan", "bu", "yao", "mima", "ku", "hong", "china", "xiaoxiao", "fore", "qing", "qiang", "xing", "nu", "yangyang", "ting", "baobao", "dai", "bao", "ou", "kai", "ran", "xi", "shan", "qin", "chao", "mei", "qian", "jiajia", "che", "maomao", "mao", "laopo", "feifei", "ru", "ken", "bing", "xiaoyu", "ding", "buzhidao", "san", "lai", "wangwei", "wocaonima", "sheng", "lili", "woshishui", "yin", "zhong", "zhangwei", "liwei", "zi", "wai", "deng", "woainima", "ping", "can", "shuai", "baobei", "cu", "qiu", "game", "liao", "ning", "pu", "sai", "xuan", "zhi", "kang", "chi", "zhanglei", "rong", "zhuzhu", "ao", "like", "lue", "mai", "xiaowei", "ben", "jiao", "wangjian", "hen", "wangyu", "zhen", "xiaolong", "tianshi", "zhangyu", "gen", "cha", "wode", "wanglei", "cui", "liuyang", "lie", "linlin", "zeng", "shuang", "lian", "lia", "rui", "xiong", "ei", "chang", "qiqi", "worinima", "haha", "shao", "wangyang", "huan", "bobo", "jingjing", "hou", "yong", "pen", "lou", "lulu", "zhangjian", "lang", "tong", "liuwei", "liyang", "juan", "miao", "lijian", "shu", "waini", "woailaopo", "dandan", "xinxin", "niu", "duan", "ban", "nihaoma", "tiancai", "zhangjie", "wenwen", "hang", "yuanyuan", "wojiushiwo", "xiaofeng", "yanyan", "shang", "lijun", "zou", "wodemima", "gang", "tiantian", "mama", "nie", "xixi", "mile", "gan", "lele", "xiaoyao", "qiao" };
        //库里常见拼音，作为密码第一个组成部分
        string[] pinyin = {"woaini", "woaiwojia", "nihao", "caonima", "cao", "mima", "laopo", "buzhidao", "wocaonima", "woshishui", "woainima", "baobei", "zhuzhu", "tianshi", "wode",
            "worinima", "haha", "waini", "woailaopo", "nihaoma", "tiancai", "wojiushiwo", "wodemima", "mama" };
        //常见前缀，作为密码第一个组成部分
        string[] prefix = {"a","A","q","Q","QQ","qq","1","abc","woai","asd","qwe","qaz",
                           "woshi","iloveyou","love","ilove"};
        //常见后缀，作为密码第二个组成部分
        string[] subfix =  { "!", "@", "#", "%", "a", "A","abc","qq",
                                    "0","1", "2", "3", "4", "5", "6", "7", "8", "9",
                                    "00","11","22","33","44","55","66","77","88","99"};
        //常见数字，添加到数字列表里
        string[] shuzi_mini = { "123","1234","12345","123456","1234567","12345678","123456789","1234567890","0123456789",
                            "147","258","369","168","520","1314","5201314","1314520","201314",
                            "000","111", "222","333","444","555","666","777","888","999","007",
                            "1921","1949","1976","1979","1024","2048"
                            };
        //常见数字，添加到数字列表里
        string[] shuzi = { "123","1234","12345","123456","1234567","12345678","123456789","1234567890","0123456789",
                            "147","258","369","168","520","1314","5201314","1314520","201314",
                            "000","0000","00000","000000","111","1111","11111","111111",
                            "222","2222","22222","222222","333","3333","33333","333333",
                            "444","4444","44444","444444","555","5555","55555","555555",
                            "666","6666","66666","666666","777","7777","77777","777777",
                            "888","8888","88888","888888","999","9999","99999","999999" ,
                            "1921","1949","1976","1979","1024","2048",
                            "1980","1981","1982","1983","1984","1985","1986","1987","1988","1989",
                            "1990","1991","1992","1993","1994","1995","1996","1997","1998","1999",
                            "2000","2001","2002","2003","2004","2005","2006","2007","2008","2009",
                            "2010","2011","2012","2013","2014","2015","2016","2017","2018","2019",
                            "2020","2021","2022","2023"};

        /******************************************************************************************
        1.数字占绝大部分：d6（身份证前后六位，123456类的弱密码，YYYYMM，YYMMDD，YYYYMD，手机号前六位），
                          d8（YYYYMMDD，MMDDYYYY，qq号），
                          d7（YYYYMDD，YYYYMMD，5201314），
                          d9（qq号，生日加1位），
                          d11（手机号），
                          d10（手机号前10位），
                          l8（姓名）...
        2.长度：9,8,7,10,11,12,13,14,15,16,17...
        3.小写字母在字母中占大部分比例
        4.日期："YYYYMMDD","MMDDYYYY","YYYYMDD","YYYYMMD","YYYYMM","YYYYMD","YYMMDD","YYYY","MMDD","YYMM"
          也就是YMD,MDY,YM,Y,MD,YM
         ******************************************************************************************/

        /******************************************************************************************
        生成方法：
        1.top300，keyboard，pinyin
        1.单一信息变形（手机号，qq号，生日，姓名，身份证号，社工库泄露的密码）
        2.两种信息结合（键盘模式，姓名，生日，手机，身份证号，qq号，城市，id，特殊年份，社工库泄露的密码，[0-9,a-z,A-Z]）
        *******************************************************************************************/
        public Form1()
        {
            InitializeComponent();

        }
        //用于取姓名首字母
        private string getAllUppercase(string str)
        {
            string res = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 'A' && str[i] <= 'Z')
                {
                    res = res + str[i]+"";
                }
            }
            return res;
        }
        private List<string> genFromOneDate(date d)
        {
            List<string> res = new List<string>();
            List<string> year_tmp = new List<string>();
            List<string> month_tmp = new List<string>();
            List<string> day_tmp = new List<string>();
            string year = d.year;
            string month = d.month;
            string day = d.day;

            if (year.Length == 0) { return res; }
            if (month.Length == 0) { res.Add(year); return res; }
            if (day.Length == 0) { res.Add(year + month); return res; }

            if (year.Length > 0)
            {
                year_tmp.Add(year);
                year_tmp.Add(year[2] + "" + year[3]);
            }

            if (month.Length == 1)
            {
                month_tmp.Add(month);
                month_tmp.Add("0" + month[0]);
            }
            if (month.Length == 2)
            {
                month_tmp.Add(month);
            }
            if (day.Length == 1)
            {
                day_tmp.Add(day);
                day_tmp.Add("0" + day[0]);
            }
            if (day.Length == 2)
            {
                day_tmp.Add(day);
            }
            for (int i = 0; i < year_tmp.Count; i++)
            {
                for (int j = 0; j < month_tmp.Count; j++)
                {
                    for (int k = 0; k < day_tmp.Count; k++)
                    {
                        res.Add(year_tmp[i] + "" + month_tmp[j] + "" + day_tmp[k]);
                    }
                }
            }
            return res;
        }
        private List<string> genFromAllDate(date d1, date d2, date d3)
        {
            List<string> res = new List<string>();
            res = res.Union(genFromOneDate(d1)).Union(genFromOneDate(d2)).Union(genFromOneDate(d3)).ToList<string>();
            return res;
        }
        private List<string> genFromOneName(name n)
        {
            List<string> res = new List<string>();
            List<string> xing_tmp = new List<string>();
            List<string> ming_tmp = new List<string>();

            string X = getAllUppercase(n.xing);//大写标记原来姓名开始的字母，这里是首字母缩写
            string M = getAllUppercase(n.ming);
            string xing = n.xing.ToLower();//转变成小写字母
            string ming = n.ming.ToLower();

            if (xing.Length == 0) { return res; }
            if (ming.Length == 0) { res.Add(ming); return res; }

            xing_tmp.Add(xing);//全小写
            //xing_tmp.Add(xing.ToUpper());//全大写
            xing_tmp.Add(xing[0].ToString().ToUpper() + xing.Substring(1));//第一字母大写
            ming_tmp.Add(ming);
            //ming_tmp.Add(ming.ToUpper());
            ming_tmp.Add(ming[0].ToString().ToUpper() + ming.Substring(1));//第一字母大写
            ming_tmp.Add(ming);//hh
            /*
            //逐个变为大写
            for (int i = 0; i < xing.Length; i++)
            {
                xing_tmp.Add(xing.Substring(0,i)+xing[i].ToUpper()+xing.Substring(i+1));
            }
            for (int i = 0; i < ming.Length; i++)
            {
                ming_tmp.Add(ming.Substring(0, i) + ming[i].ToUpper() + ming.Substring(i + 1));
            }
            */
            //缩写变为全大写和全小写
            /*
            xing_tmp.Add(X.ToLower());
            xing_tmp.Add(X.ToUpper());
            ming_tmp.Add(M.ToLower());
            ming_tmp.Add(M.ToUpper());
            */
            for (int i = 0; i < xing_tmp.Count; i++)
            {
                res.Add(xing_tmp[i]);
            }
            for (int i = 0; i < ming_tmp.Count; i++)
            {
                res.Add(ming_tmp[i]);
            }
            for (int i = 0; i < xing_tmp.Count; i++)
            {
                for (int j = 0; j < ming_tmp.Count; j++)
                {
                    res.Add(xing_tmp[i] + "" + ming_tmp[j]);
                    res.Add(ming_tmp[j] + "" + xing_tmp[i]);
                }
            }
            res.Add(X.ToLower() + "" + M.ToLower());//hhh
            res.Add(X.ToUpper() + "" + M.ToLower());//Hhh
            res.Add(X.ToUpper() + "" + M.ToUpper());//HHH
            return res;
        }
        private List<string> genFromAllName(name n1, name n2, name n3)
        {
            List<string> res = new List<string>();
            res = res.Union(genFromOneName(n1)).Union(genFromOneName(n2)).Union(genFromOneName(n3)).ToList<string>();
            return res;
        }
        private List<string> genFromOneNumber(string num)//6位到6位的数字
        {
            List<string> res = new List<string>();

            if (num.Length == 0) { return res; }
            if (num.Length <= 6) { res.Add("" + num); return res; }
            res.Add("" + num);
            for (int i = 6; i <= 6; i++)
            {
                for (int j = 0; j + i <= num.Length; j++)
                {
                    res.Add(num.Substring(j, i));
                }
            }
            return res;
        }
        private List<string> genFromAllNumbers(string phone1, string phone2, string phone3, string phone4, string qq1, string qq2, string qq3, string qq4, string shenfenzheng)
        {
            List<string> res = new List<string>();
            if (use_big_shuzi)
            {
                for (int i = 0; i < shuzi.Length; i++)
                {
                    res.Add(shuzi[i]);
                }
            }
            else
            {
                for (int i = 0; i < shuzi_mini.Length; i++)
                {
                    res.Add(shuzi_mini[i]);
                }
            }
               
            res = res.Union(genFromOneNumber(phone1)).Union(genFromOneNumber(phone2)).Union(genFromOneNumber(phone3)).Union(genFromOneNumber(phone4)).ToList<string>();
            res = res.Union(genFromOneNumber(qq1)).Union(genFromOneNumber(qq2)).Union(genFromOneNumber(qq3)).Union(genFromOneNumber(qq4)).ToList<string>();
            res = res.Union(genFromOneNumber(shenfenzheng)).ToList<string>();
            return res;
        }
        private List<string> genFromKeyboard(string[] kbdpattern)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < kbdpattern.Length; i++)
            {
                res.Add(kbdpattern[i]);
            }
            return res;
        }
        private List<string> genFromPinYin(string[] pinyin)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < pinyin.Length; i++)
            {
                res.Add(pinyin[i]);
            }
            return res;
        }
        private List<string> genFromSheGongKu(string shegongku)
        {
            List<string> res = new List<string>();
            if (shegongku.Length == 0) { return res; }
            string[] sArray = shegongku.Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sArray.Length; i++)
            {
                res.Add(sArray[i]);
            }
            return res;
        }
        private List<string> genFromCityUniv(string city, string biyexuexiao)
        {
            List<string> res = new List<string>();
            string[] cityArray = city.Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
            string[] univArray = biyexuexiao.Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < cityArray.Length; i++)
            {
                res.Add(cityArray[i].ToLower());//全小写
                res.Add(cityArray[i].ToUpper());//全大写
                res.Add(cityArray[i][0].ToString().ToUpper() + cityArray[i].Substring(1));//第一个字母大写
            }
            for (int i = 0; i < univArray.Length; i++)
            {
                res.Add(univArray[i].ToLower());//全小写
                res.Add(univArray[i].ToUpper());//全大写
                res.Add(univArray[i][0].ToString().ToUpper() + univArray[i].Substring(1));//第一个字母大写
            }
            return res;
        }
        private List<string> genFromPrefix(string[] prefix)
        {
            List<string> res = new List<string>();
            if (prefix.Length == 0) { return res; }
            for (int i = 0; i < prefix.Length; i++)
            {
                res.Add(prefix[i]);
            }
            return res;
        }
        private List<string> genFromSubfix(string[] subfix)
        {
            return genFromPrefix(subfix);
        }
        private List<string> genFromChangYongID(string id)
        {
            List<string> res = new List<string>();
            if (id.Length != 0)
            {
                string[] sArray = id.Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < sArray.Length; i++)
                {
                    res.Add(sArray[i]);
                }
            }
            return res;
        }
        private List<string> genFromOtherInfo(string info)
        {
            List<string> res = new List<string>();
            if (info.Length != 0)
            {
                string[] sArray = info.Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < sArray.Length; i++)
                {
                    res.Add(sArray[i]);
                }
            }
            return res;
        }

        private void writeLog(StreamWriter s,string log) {
            s.WriteLine("*************************" + log+"-----------------------");
        }
        private void writeList(StreamWriter s, List<string> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Length >= THRESHOLD)
                {
                    s.WriteLine(l[i]); //write list
                }
            }
        }
        //order 表示严格按照参数的顺序来合并
        private void writeLists(StreamWriter s,string log, List<string> l1, List<string> l2,bool order=false)
        {
            writeLog(s, log);
            for (int i = 0; i < l1.Count; i++)
            {
                for (int j = 0; j < l2.Count; j++)
                {
                    string tmp1 = l1[i] + l2[j];
                    string tmp3 = l2[j] + l1[i];
                    if (tmp1.Length >= THRESHOLD)
                    {
                        s.WriteLine(tmp1); //l1+l2
                    }

                    if (tmp3.Length >= THRESHOLD && (!order))
                    {
                        s.WriteLine(tmp3); //l2+l1
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            include_top_300 = this.checkBox5.Checked;//生成的字典含有top300
            use_big_shuzi = this.checkBox5.Checked;//不使用大数字数组，使用shuzi_mini[]
            names[0].xing = this.textBox1.Text;
            names[0].ming = this.textBox2.Text;
            names[1].xing = this.textBox3.Text;
            names[1].ming = this.textBox4.Text;
            names[2].xing = this.textBox5.Text;
            names[2].ming = this.textBox6.Text;

            birthdays[0].year = this.textBox7.Text;
            birthdays[0].month = this.textBox8.Text;
            birthdays[0].day = this.textBox9.Text;
            birthdays[1].year = this.textBox10.Text;
            birthdays[1].month = this.textBox11.Text;
            birthdays[1].day = this.textBox12.Text;
            birthdays[2].year = this.textBox13.Text;
            birthdays[2].month = this.textBox14.Text;
            birthdays[2].day = this.textBox15.Text;

            phones[0] = this.textBox16.Text;
            phones[1] = this.textBox18.Text;
            phones[2] = this.textBox20.Text;
            phones[3] = this.textBox22.Text;

            qqs[0] = this.textBox17.Text;
            qqs[1] = this.textBox19.Text;
            qqs[2] = this.textBox21.Text;
            qqs[3] = this.textBox23.Text;

            identity = this.textBox24.Text;
            city = this.textBox25.Text;
            biyexuexiao = this.textBox26.Text;
            changyongID = this.textBox27.Text;
            shegongku = this.textBox28.Text;
            otherinfo = this.textBox29.Text;
            FileName = this.textBox30.Text;
            if (FileName != "")
            {
                StreamWriter myStream;
                myStream = new StreamWriter(FileName);

                List<string> birth = genFromAllDate(birthdays[0], birthdays[1], birthdays[2]);
                List<string> name = genFromAllName(names[0], names[1], names[2]);
                List<string> number = genFromAllNumbers(phones[0], phones[1], phones[2], phones[3], qqs[0], qqs[1], qqs[2], qqs[3], identity);
                List<string> kbd = genFromKeyboard(keyboard);
                List<string> py = genFromPinYin(pinyin);
                List<string> shegong = genFromSheGongKu(shegongku);
                List<string> cityanduniv = genFromCityUniv(city, biyexuexiao);
                List<string> qianzhui = genFromPrefix(prefix);
                List<string> houzhui = genFromSubfix(subfix);
                List<string> changyongid = genFromChangYongID(changyongID);
                List<string> info = genFromOtherInfo(otherinfo);
                /*****************************************************************************
                name birth  number kbd py shegong cityanduniv qianzhui houzhui changyongid info
                *******************************************************************************/
                writeList(myStream, name);//names
                writeList(myStream, birth);//birthdays
                writeList(myStream, number);//qqs and phones and shenfenzheng and shuzi
                writeList(myStream, kbd);//keyboard
                writeList(myStream, py);//pinyin
                writeList(myStream, shegong);//shegong
                writeList(myStream, cityanduniv);//city and univ
                writeList(myStream, changyongid);//changyongid

                writeLists(myStream, "prefix name", qianzhui, name, true);
                writeLists(myStream, "prefix birth", qianzhui, birth, true);
                writeLists(myStream, "prefix number", qianzhui, name, true);
                writeLists(myStream, "prefix pinyin", qianzhui, py, true);
                writeLists(myStream, "prefix shegong", qianzhui, shegong, true);
                writeLists(myStream, "prefix cityuniv", qianzhui, cityanduniv, true);
                writeLists(myStream, "prefix changyongid", qianzhui, changyongid, true);

                writeLists(myStream, "name subfix", name,  houzhui,true);
                writeLists(myStream, "birth subfix",  birth,houzhui, true);
                writeLists(myStream, "number subfix",  name,houzhui, true);
                writeLists(myStream, "pinyin subfix",  py,houzhui, true);
                writeLists(myStream, "shegong subfix",  shegong,houzhui, true);
                writeLists(myStream, "cityuniv subfix",  cityanduniv,houzhui, true);
                writeLists(myStream, "changyongid subfix",  changyongid, houzhui,true);

                writeLists(myStream, "name birthday", name, birth);
                writeLists(myStream, "name number", name, number);
                writeLists(myStream, "name pinyin", name, py);
                writeLists(myStream, "name keyboard", name, kbd);
                writeLists(myStream, "name shegong", name, shegong);
                writeLists(myStream, "name cityuniv", name, cityanduniv);

                writeLists(myStream, "birth kbd", birth, kbd);
                writeLists(myStream, "birth shegong", birth, shegong);
                writeLists(myStream, "birth cintyanduniv", birth, cityanduniv);
                
                writeLists(myStream, "number kbd", number, kbd);
                writeLists(myStream, "number pinyin", number, py);
                writeLists(myStream, "number shegong", number, shegong);
                writeLists(myStream, "number cityanduniv",number, cityanduniv);
                
                writeLists(myStream,"kbd shegong", kbd, shegong);
                writeLists(myStream,"kbd cityanduniv", kbd, cityanduniv);
                
                writeLists(myStream, "shegong cityanduniv",shegong, cityanduniv);
                if (include_top_300)
                {
                    for (int i = 0; i < top300.Length; i++)
                    {
                        myStream.WriteLine(top300[i]); //top300
                    }
                }
                
                myStream.Close();//关闭流
                MessageBox.Show("字典生成完毕！");
            }
            else
            {
                MessageBox.Show("请选择字典存放位置！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                FileName = saveFileDialog.FileName;
            }
            else
            {
                FileName = "";
            }
            this.textBox30.Text = FileName;
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            string s = textBox31.Text;
            textBox32.Text = s.Length+"";
            List<bool> comp = componets(s);
            checkBox1.Checked = comp[0];
            checkBox2.Checked = comp[1];
            checkBox3.Checked = comp[2];
            checkBox4.Checked = comp[3];
            //textBox33.Text = componets(s);
            //textBox34.Text = componets(s);
        }
        private List<bool> componets(string password)
        {
            List<bool> res = new List<bool>();
            bool number = false, letter = false, Letter = false, sign = false;
            foreach(char c in password)
            {
                if (map(c) == 'n') { number = true; }
                if (map(c) == 'l') { letter = true; }
                if (map(c) == 'L') { Letter = true; }
                if (map(c) == 's') { sign = true; }
            }
            res.Add(number);
            res.Add(letter);
            res.Add(Letter);
            res.Add(sign);
            return res;
        }
        private char map(char c)
        {
            //char c = ch[0];
            if(c>='a' && c <= 'z')
            {
                return 'l';
            }
            if (c >= 'A' && c <= 'Z')
            {
                return 'L';
            }
            if (c >= '0' && c <= '9')
            {
                return 'n';
            }
            else
            {
                return 's';
            }
        }
    }
}
