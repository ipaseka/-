using _1.ATM_Info.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace _1.ATM_Info
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
        
    //Разработать приложение, имитирующее работу банкомата.
    //Изначально имется несколько коллекций с заранее заполненными данными, имитирущими таблицы в базе данных

    //Коллекция Пользователи - хранит информаци о польователях, зарегистрированных в банке
    //List<User> = ..
    //Id, Имя, фамилия, отчество, телефон, данные паспорта, дата регистрации, логин, пароль
    //Коллекция Счета - хранит информацию о счетах польователей, у каждого пользователя может быть несколько счетов.
    //List<Account>=...
    //Id, дата открытия счёта, сумма на счёте, Id владельца
    //Каждый счёт связан с пользователем по Id
    //Коллекция История операций - хранит истори о всех операциях с конкретным счётом, имется 2 типа операции, пополнение и вывод денег со счёта.
    //List<History> = ...
    //Id, дата операции, тип операции, сумма, Id счёта
    //Каждая запись в истории счвязана с конкретным счётом

    //LINQ запросы:
    //1. Вывод информации о заданном аккаунте по логину и паролю
    //2. Вывод данных о всех счетах заданного пользователя
    //3. Вывод данных о всех счетах заданного пользователя, включая историю по каждому счёту
    //4. Вывод данных о всех операциях пополенения счёта с указанием владельца каждого счёта
    //5. Вывод данных о всех пользователях у которых на счёте сумма больше N (N задаётся из вне и может быть любой)
    public partial class MainWindow : Window
    {
        private List<User> userList = default;
        private List<Account> accountList = default;
        private List<CashFlow> cashFlowList = default;
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            userList = User.GetDefaultList();
            accountList = Account.GetDefaultList();
            cashFlowList = CashFlow.GetDefaultList();
        }

        private void OnBtnFind_Click(object sender, RoutedEventArgs e)
        {
            string errString = default;
            if (tbLogin.Text.Length == 0)
                errString = "Login is required";
            if (tbPassword.Password.Length == 0)
                errString = "Password is required";
            if (errString != default)
            {
                MessageBox.Show(errString, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var tmpUserList = userList.Where(x => x.Login == tbLogin.Text && x.Password == tbPassword.Password);
            dgUser.ItemsSource = tmpUserList;
            dgAccount.ItemsSource = accountList.Join(tmpUserList, x => x.UserId, y => y.Id, (x, y) => x).Select(x => new AccountView { Id = x.Id, Date = x.Date, Amount = x.Amount(cashFlowList) });
        }
        private void OnDgAccount_SelectionChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count != 1)
            {
                dgDeposits.ItemsSource = null;
                dgWithdrawals.ItemsSource = null;
                return;
            }
            int accId = (e.AddedItems[0] as AccountView).Id;
            dgDeposits.ItemsSource = cashFlowList.Where(x => x.AccountId == accId && x.Type == CashFlowType.Deposit);
            dgWithdrawals.ItemsSource = cashFlowList.Where(x => x.AccountId == accId && x.Type == CashFlowType.Withdrawals);
        }

        private void OnBtnFindBySum_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(tbSum.Text, out decimal val))
                MessageBox.Show("Wrond Number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                dgUserBySum.ItemsSource = accountList.Where(x => x.Amount(cashFlowList) >= val).Select(x => x.UserId).Distinct().Join(userList, x => x, y => y.Id, (x, y) => y);
        }
    }
}
