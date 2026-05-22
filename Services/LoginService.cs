using System.Collections.Generic;

namespace SpecFlowLoginDemo.Services
{
    // Class nay luu ket qua tra ve sau khi dang nhap
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        // Function khoi tao ket qua dang nhap
        public LoginResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

    // Class nay chua toan bo logic xu ly dang nhap
    public class LoginService
    {
        // Tai khoan hop le trong demo
        private const string ValidUsername = "admin";
        private const string ValidPassword = "123456";

        // So lan sai toi da truoc khi khoa tai khoan
        private const int MaxFailedAttempts = 3;

        // Luu so lan dang nhap sai cua tung user
        private readonly Dictionary<string, int> _failedAttempts = new Dictionary<string, int>();

        // Luu danh sach user da bi khoa
        private readonly HashSet<string> _lockedUsers = new HashSet<string>();

        // Function chinh de xu ly dang nhap
        public LoginResult Login(string username, string password)
        {
            // Neu gia tri null thi doi thanh chuoi rong de tranh loi
            username ??= string.Empty;
            password ??= string.Empty;

            // Kiem tra username rong
            if (string.IsNullOrWhiteSpace(username))
            {
                return new LoginResult(false, "Username is required");
            }

            // Kiem tra password rong
            if (string.IsNullOrWhiteSpace(password))
            {
                return new LoginResult(false, "Password is required");
            }

            // Neu tai khoan da bi khoa thi khong cho dang nhap nua
            if (_lockedUsers.Contains(username))
            {
                return new LoginResult(false, "Account is locked");
            }

            // Neu dung username va password thi dang nhap thanh cong
            if (username == ValidUsername && password == ValidPassword)
            {
                _failedAttempts[username] = 0;
                return new LoginResult(true, "Login successful");
            }

            // Neu sai username hoac password thi tang so lan sai
            RegisterFailedAttempt(username);

            // Sau khi tang so lan sai, neu user bi khoa thi tra ve Account is locked
            if (_lockedUsers.Contains(username))
            {
                return new LoginResult(false, "Account is locked");
            }

            // Truong hop sai thong tin nhung chua du 3 lan
            return new LoginResult(false, "Invalid username or password");
        }

        // Function nay tang so lan dang nhap sai va khoa tai khoan neu sai 3 lan
        private void RegisterFailedAttempt(string username)
        {
            if (!_failedAttempts.ContainsKey(username))
            {
                _failedAttempts[username] = 0;
            }

            _failedAttempts[username]++;

            if (_failedAttempts[username] >= MaxFailedAttempts)
            {
                _lockedUsers.Add(username);
            }
        }

        // Function nay reset du lieu truoc moi scenario de cac test khong anh huong nhau
        public void Reset()
        {
            _failedAttempts.Clear();
            _lockedUsers.Clear();
        }
    }
}
