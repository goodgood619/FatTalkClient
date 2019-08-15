using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
namespace WpfApp1.Modules
{
    public class MessengerClient : Tcpclient
    {
        public Action<TCPmessage> domessage { get; set; } = null;
        public Userdata userdata { get; set; } = new Userdata();
        private JsonHelp json = new JsonHelp();
        public MessengerClient()
        {
            //domessage = null;
            //userdata = new Userdata();
        }
        public bool requestLogin(string id, string password)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.login;
            message.message = json.logininfo(id, password);
            return Send(message);
        }

        public bool requestIdcheck(string id)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Idcheck;
            message.message = json.idcheckinfo(id);
            return Send(message);
        }
        public bool requestJoin(string id, string password, string nickname, string phone)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Join;
            message.message = json.joininfo(id, password, nickname, phone);
            return Send(message);
        }
        public bool requestNicknamecheck(string nickname)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Nicknamecheck;
            message.message = json.nicknamecheckinfo(nickname);
            return Send(message);
        }
        public bool requestFindid(string id,string phone)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Findid;
            message.message = json.Findidphoneinfo(id,phone);
            return Send(message);
        }
        public bool requestLogout(string usernickname)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.logout;
            message.message = json.nicknamecheckinfo(usernickname);
            return Send(message);
        }
        public bool requestPlusfriend(string plusfriendid, string userid)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Plusfriend;
            message.message = json.plusidcheckinfo(plusfriendid, userid);
            return Send(message);
        }
        public bool requestFreshcommand(string usernickname)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Refresh;
            message.message = json.nicknamecheckinfo(usernickname);
            return Send(message);
        }
        public bool requestDeletefriendcommand(string[] removenickarray,string nickname)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Removefriend;
            message.message = json.deletenickinfo(removenickarray,nickname);  
            return Send(message);
        }
        public bool requestMakechatcommand(string[] chatnickarray,string nickname)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Makechat;
            message.message = json.makechatnickinfo(chatnickarray, nickname);
            return Send(message);
        }
        public bool requestSendchatcommand(int chatnumber,string sendusernickname,string Message)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Sendchat;
            message.Chatnumber = chatnumber;
            message.message = json.sendchatinfo(Message, sendusernickname);
            return Send(message);
        }
        public bool requestOutchatcommand(int chatnumber,string sendusernickname)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Outchat;
            message.Chatnumber = chatnumber;
            message.message = json.nicknamecheckinfo(sendusernickname);
            return Send(message);
        }
        public bool requestJoinchatcommand(string joinchatid,int chatnumber,string usernickname)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Joinchat;
            message.Chatnumber = chatnumber;
            message.message = json.sendjoinchatinfo(joinchatid,usernickname);
            return Send(message);
        }
        public bool requestBlockfriendcommand(string blocknickname,string blockedid)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Blockfriend;
            message.message = json.sendjoinchatinfo(blockedid, blocknickname);
            return Send(message);
        }
        public bool requestNotBlockfriendcommand(string blocknickname,string blockedid)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.NotBlockfriend;
            message.message = json.sendjoinchatinfo(blockedid, blocknickname);
            return Send(message);
        }
        public bool requestRefreshchatnickarray(int chatnumber,string usernickname)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Refreshchatnickarray;
            message.Chatnumber = chatnumber;
            message.message = json.nicknamecheckinfo(usernickname);
            return Send(message);
        }
        public bool requestChangeroomname(int chatnumber,string roomname,string usernickname)
        {
            TCPmessage message = new TCPmessage();
            message.Command = Command.Changeroomname;
            message.message = json.Changeroomnameinfo(roomname,usernickname);
            return Send(message);
        }
        public override void ResponseMessage(TCPmessage message)
        {
            if (domessage != null)
            {
                domessage(message);
            }
        }
    }
}
