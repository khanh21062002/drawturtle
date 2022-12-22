#include <fstream>
#include <string.h>
#include <bits/stdc++.h>
// #include <string>
// #include <Windows.h>
// #include <stdlib.h>
#include "main_menu.h"

using namespace std;

void addData(void);
void viewData(void);
void changeData(void);
void deleteData(void);
//void admin(void);
void mainMenu(void);


void admin()
{
    system("cls");
    cout << "\n\n\t\t\t\t\t | Dang nhap voi vai tro quan tri vien |\n";
    cout << "\n\n\t\t\t\t\t 1. Them du lieu hoc sinh";
    cout << "\n\n\t\t\t\t\t 2. Xoa du lieu hoc sinh";
    cout << "\n\n\t\t\t\t\t 3. Sua du lieu hoc sinh";
    cout << "\n\n\t\t\t\t\t 4. Xem bang du lieu ";
    cout << "\n\n\t\t\t\t\t 5. Man hinh chinh ";
    cout << "\n\n\t\t\t\t\t 6. Thoat";

    int tuy_chon;
    cout << "\n\n\t\t\t\t\t Moi ban chon so : ";
    do
    {
        cin >> tuy_chon;
        {
            switch (tuy_chon)
            {
            case 1:
                addData();
                break;
            
            case 2: 
                deleteData();
                break;
            case 3:
                changeData();
                break;
            case 4:
                viewData();
                break;
            case 5:
                mainMenu();
                break;
            case 6:
                system("cls");
                cout << "\t\t\n\n\n\n\n\n\n\t\t\t\t\tThoat chuong trinh";

                for (int i = 0; i < 4; i++)
                {

                    Sleep(1000);
                    cout << ".";
                }
                exit(0);
                break;
            default:
                system("cls");
                cout << "\n\n\t\t\t\t\t | Dang nhap voi vai tro admin |\n";
                cout << "\n\n\t\t\t\t\t 1. Them du lieu hoc sinh";
                cout << "\n\n\t\t\t\t\t 2. Xoa du lieu hoc sinh";
                cout << "\n\n\t\t\t\t\t 3. Sua du lieu hoc sinh";
                cout << "\n\n\t\t\t\t\t 4. Xem bang du lieu ";
                cout << "\n\n\t\t\t\t\t 5. Man hinh chinh ";
                cout << "\n\n\t\t\t\t\t 6. Thoat";

                cout << "\n\n\t\t\t\t\t Ban da nhap sai dau vao, vui long nhap lai !";

                cout << "\n\n\t\t\t\t\t Moi ban chon so : ";
            }
        }
    } while (tuy_chon != '6');
}
void addData()
{
    system("cls");
    fstream file("data.txt", ios::out | ios::app);
    if (!file)
    {
        cout << "\nLoi khong mo duoc data.txt !";
    }
    else
    {
        cout << "\t|nhap du lieu|"<< endl << endl ;
    }
    string id, name , email , phone, bill;
    string moreData;

    cout << "\n Nhap so ID : ";
    cin >> id;
    file << endl
         << id << ", ";

    cout << "\n Nhap ten sinh vien : ";
    cin >> name;
    file << name << ", ";

    cout << "\n Nhap Email : ";
    cin >> email;
    file << email << ", ";

    cout << "\n Nhap so dien thoai : ";
    cin >> phone;
    file << phone << ", ";

    cout << "\n Nhap so tien thanh toan : ";
    cin >> bill;
    file << bill << ", ";

    file.close();

    admin();
}

void deleteData()
{
    system("cls");

    fstream fin, fout;

    fin.open("data.txt", ios::in);
    fout.open("data_new.txt", ios::out);

    int roll_num, roll_1, marks, count = 0, i;
    char sub;
    int index, new_marks;
    string line, word;
    vector<string> row;

    cout << "Nhap so dong can xoa : ";
    cin >> roll_num;

     while (!fin.eof())
    {

        row.clear();

        getline(fin, line);
        stringstream s(line);

        while (getline(s, word, ','))
        {
            row.push_back(word);
        }

        int row_size = row.size();
        roll_1 = stoi(row[0]);

        
        if (roll_1 != roll_num)
        {
            if (!fin.eof())
            {
                for (i = 0; i < row_size - 1; i++)
                {
                    fout << row[i] << ",";
                }
                fout << row[row_size - 1] << "\n";
            }
        }
        else
        {
            count = 1;
        }
        if (fin.eof())
            break;
    }
    if (count == 1)
        cout << "Dong duoc chon da duoc xoa\n";
    else
        cout << "Khong tim thay dong duoc chon\n";

    // Close the pointers
    fin.close();
    fout.close();

    // removing the existing file
    remove("data.txt");

    // renaming the new file with the existing file name
    rename("data_new.txt", "data.txt");

    Sleep(2000);

    admin();
}

void changeData()
{
    system("cls");
    fstream fin, fout;

    fin.open("data.txt", ios::in);
    fout.open("data_new.txt", ios::out);

    int roll_num, roll_1, marks, count = 0, i;
    char sub;
    int index;
    string new_marks;
    string line, word;
    vector<string> row;

    cout << "Nhap so thu tu dong can thay doi du lieu : ";
    cin >> roll_num;

    cout << "Nhap truong thuoc tinh de thay doi du lieu (N/E/P/B or n/e/p/b or Name/Email/Phone/Bill or name/email/phone/bill): ";
    cin >> sub;

    if (sub == 'n' || sub == 'N' || sub == "Name" || sub == "name")
    {
        index = 1;
    }

    else if (sub == 'e' || sub == 'E' || sub == "Email" || sub = "email")
    {
        index = 2;
    }

    else if (sub == 'p' || sub == 'P' || sub == "Phone" || sub = "phone")
    {
        index = 3;
    }

    else if (sub == 'b' || sub == 'B' || sub == "Bill" || sub = "bill")
    {
        index = 4;
    }

    else 
    {
        cout << "Ban da chon sai, xin moi ban chon lai !"
        changeData();
    }

    cout << "Nhap du lieu moi: ";
    cin >> new_marks;

    while (!fin.eof())
        {
            row.clear();
            getline(fin, line);
            stringstream s(line);
            while (getline(s, word, ','))
            {
                row.push_back(word);
            }
            roll_1 = stoi(row[0]);
            int row_size = row.size();
            if (roll_1 == roll_num)
            {
                count = 1;
                stringstream convert;
               
                convert << new_marks;
                
                row[index] = convert.str();
                if (!fin.eof())
                {
                    for (i = 0; i < row_size - 1; i++)
                    {
                        fout << row[i] << ", ";
                    }
                    fout << row[row_size - 1] << "\n";
                }
            }
            else
            {
                if (!fin.eof())
                {
                    for (i = 0; i < row_size - 1; i++)
                    {
                        
                        fout << row[i] << ", ";
                    }
                    
                    fout << row[row_size - 1] << "\n";
                }
            }
            if (fin.eof())
                break;
        }
        if (count == 0)
            cout << "Khong tim thay";
        fin.close();
        fout.close();
        
        remove("data.txt");
       
        rename("data_new.txt", "data.txt");

        admin();
}

void viewData()
{
    system("cls");

    fstream file("data.txt");

    cout << "\n\t\t\t\t\t\t|Danh sach sinh vien| \n\n";

    cout << "\n-----------------------------------------------------------------------------------------------------------------------\n"
         << endl
         << endl;
    cout << "ID \t Name \t Email \t Phone \t Bill "<< endl
         << endl;
    string id,name,email,phone,bill;
    while (!file.eof())
    {
        getline(file , id , ',');
        getline(file , name , ',');
        getline(file , email , ',');
        getline(file , phone , ',');
        getline(file , bill , ',');

        cout << id << " \t " << name << "\t\t\t" << email << " \t\t" << phone << " \t\t" << bill << endl;
        cout << endl;
    }
    file.close();
    admin();
}







