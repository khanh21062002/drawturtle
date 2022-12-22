#include<bits/stdc++.h>
// #include <iostream>
#include <conio.h>
// #include <stdio.h>
// #include <fstream>
// #include <stdlib.h>
// #include <cstdlib>
// #include <string>
// #include <string_view>
// #include <regex>
// #include <iomanip>

using namespace std;

bool Email_check(string email)
{
    const regex pattern("(\\w+)(\\.|_)?(\\w*)@(\\w+)(\\.(\\w+))+");
    return regex_match(email, pattern);
}

class student
{
    private:
       string name, email, phone, bill;
       long long int id;
    public: 
       void main_menu();
       void insert();
       void display();
       void modify();
       void search();
       void deleted();
};
void student:: main_menu()
{
menu_start:
    char x;
    int choice;
    system("cls");
    cout << "\t\t\t-----------------------------" << endl;
    cout << "\t\t\t| HE THONG QUAN LI SINH VIEN |" << endl;
    cout << "\t\t\t-----------------------------" << endl;
    cout << "\t\t\t 1. Nhap du lieu moi" << endl;
    cout << "\t\t\t 2. Hien thi du lieu" << endl;
    cout << "\t\t\t 3. Sua du lieu" << endl;
    cout << "\t\t\t 4. Tim kiem du lieu" << endl;
    cout << "\t\t\t 5. Xoa du lieu" << endl;
    cout << "\t\t\t 6. Thoat chuong trinh\n"
         << endl;

    cout << "\t\t\t............................" << endl;
    cout << "\t\t\tChon so chuc nang :[1/2/3/4/5/6]" << endl;
    cout << "\t\t\t............................" << endl;
    cout << " Nhap lua chon cua bn: ";
    cin >> choice ;
    switch (choice)
    {
    case 1 :
        do
        {
            insert();
            cout << "\n\n\t\t\t Them du lieu hoc sinh khac ? (Y, N) : ";
            cin >> x;
        } while (x == 'y' || x == 'Y');
        break;
    case 2:
        display();
        break;
    case 3:
        modify();
        break;
    case 4:
        search();
        break;
    case 5:
        deleted();
        break;
    case 6:
        cout << "\n\t\t\t Thoat Chuong trinh";
        exit(0);
    default:
        cout << "\n\t\t\t Lua chon sai... Moi ban chon lai: ";
        break;
    }
    getch();
    goto menu_start;     
}

void student::insert()
{
    system("cls");
    fstream file;
    cout << "\n-------------------------------------------------------------------------------------------------------" << endl;
    cout << "------------------------------------- Them du lieu sinh vien ---------------------------------------------" << endl;
    cout << "\t\t\t Nhap id: ";
    cin >> id;
    //cin.ignore();
    cout << "\t\t\t Nhap ten: ";
    cin >> name;
    //getline(cin,name);
Email:
    cout << "\t\t\t Nhap Email: ";
    cin >> email;
    if(Email_check(email))
    {
        cout << "\t\t\t Email cua ban hop le " << endl;
    }
    else
    {
        cout << "\t\t\t Email cua ban khong hop le " << endl;
        goto Email;
    }

    cout << "\t\t\t Nhap so dien thoai: " ;
    cin >> phone;

    cout << "\t\t\t Nhap Bill: ";
    cin >> bill; 

    file.open("data.txt", ios::app | ios::out);
    file << id << " " << name << " " << email << " " << phone << " " << bill <<  endl ;
    file.close();
}

void student::display()
{
    system("cls");
    fstream file ;
    cout << "\n-------------------------------------------------------------------------------------------------------" << endl;
    cout << "------------------------------------- Bang danh sach sinh vien --------------------------------------------" << endl;
    file.open("data.txt",ios::in);
    if (!file)
    {
        cout << "\n\t\t\tHien tai khong co du lieu... ";
        file.close();
    }
    else
    {
        file >> id >> name >> email >> phone >> bill ;
        cout << setw(5)  << left << "ID";
        cout << setw(30) << left << "Name";
        cout << setw(40) << left << "Email";
        cout << setw(13) << left << "Phone";
        cout << setw(12) << left << "Bill" << endl;
        cout << setfill('-');
	    cout << setw(100) << "-" << endl;
        cout << setfill(' ');
        while (!file.eof())
        {
            cout << setw(5)  << left << id;
            cout << setw(30) << left << name;
            cout << setw(40) << left << email;
            cout << setw(13) << left << phone;
            cout << setw(12) << left << bill << endl;
            cout << endl;
            file >> id >> name >> email >> phone >> bill ;
        }
    }
    file.close();
}

void student::modify()
{
    system("cls");
    fstream file, file_1;
    long long int roll_no;
    int found = 0;
    cout << "\n-------------------------------------------------------------------------------------------------------" << endl;
    cout << "------------------------------------- Sua du lieu sinh vien ------------------------------------------" << endl;
    file.open("data.txt", ios::in);
    if(!file)
    {
        cout << "\n\t\t\tHien tai khong co du lieu... ";
        file.close();
    }
    else
    {
        cout << "\n Chon so dong ma ban muon sua doi: ";
        cin >> roll_no;
        file_1.open("new_data.txt", ios::app | ios::out);
        file >> id >> name >> email >> phone >> bill ;
        while (!file.eof())
        {
            if(roll_no != id){
                file_1<< " " << id << " " << name << " " << email << " " << phone << " " << bill << "\n";
            }
            else
            {
                cout << "\n\t\t\tNhap ID: ";
                cin >> id;
                cout << "\t\t\tNhap ten: ";
                cin >> name;
            Email:
                cout << "\t\t\tNhap email(name@gmail.com): ";
                cin >> email;
                if (Email_check(email))
                {
                    cout << "\t\t\t Email cua ban hop le" << endl;
                }
                else
                {
                    cout << "\t\t\t Email cua ban khong hop le" << endl;
                    goto Email;
                }

                cout << "\n\t\t\tNhap SDT: ";
                cin >> phone;

                cout << "\n\t\t\tNhap Bill: ";
                cin >> bill;
                file_1<< " " << id << " " << name << " " << email << " " << phone << " " << bill << "\n";
                found++;
                cout << "\n\t\t\t Sua du lieu thanh cong...";
            }
            file >> id >> name >> email >> phone >> bill ;
        }
        if(found == 0)
        {
            cout << "\n\n\t\t\t Khong co du lieu";
        }
        file_1.close();
        file.close();
        remove("data.txt");
        rename("new_data.txt", "data.txt");
    }
}

void student::search()
{
    system("cls");
    fstream file;
    int found = 0;
    cout << "\n-------------------------------------------------------------------------------------------------------" << endl;
    cout << "------------------------------------- Tim kiem du lieu ------------------------------------------" << endl;
    file.open("data.txt", ios::in);
    if ( !file )
    {
        cout << "\n\t\t\tHien tai khong co du lieu " << endl;
        file.close();
    }
    else
    {
        long long int rollno;
        cout << "\nNhap so dong ma ban muon tim : ";
        cin >> rollno;
        file >> id >> name >> email >> phone >> bill ;
        cout << "ID\tName\tEmail\t\t\t\tPhone\t\t\tBill "<< endl
             << endl;
        while (!file.eof())
        {
            if(rollno == id)
            {
                cout << id << "\t" << name << "\t" << email << "\t\t" << phone << "\t" << bill << endl;
                cout << endl;
                found++;
            }
            file >> id >> name >> email >> phone >> bill ;
        }
        if (found == 0)
        {
                cout << "\n\t\t\t Du lieu sinh vien ma ban can tim khong co ...";
        }
        file.close();
    }
}
void student::deleted()
{
    system("cls");
    fstream file, file_1;
    int found = 0;
    long long int roll_no;
    cout << "\n-------------------------------------------------------------------------------------------------------" << endl;
    cout << "------------------------------------- Xoa du lieu sinh vien ------------------------------------------" << endl;
    file.open("data.txt", ios::in);
    if (!file)
    {
        cout << "\n\t\t\tHien tai khong co du lieu";
        file.close();
    }
    else
    {
        cout << "\nNhap so dong ma ban muon xoa: ";
        cin >> roll_no;
        file_1.open("new_data.txt", ios::app | ios::out);
        file >> id >> name >> email >> phone >> bill ;
        while (!file.eof())
        {
            if (roll_no != id)
            {
                file_1<< " " << id << " " << name << " " << email << " " << phone << " " << bill << "\n";
            }
            else
            {
                found++;
                cout << "\n\t\t\t Xoa du lieu thanh cong!!!";
            }
            file >> id >> name >> email >> phone >> bill ;
        }
        if (found == 0)
        {
            cout<< "\n\t\t\t So dong ma ban can xoa khong tim thay ...";
        }
        
        file_1.close();
        file.close();
        remove("data.txt");
        rename("new_data.txt", "data.txt");
    }
}
main()
{
    student project;
    project.main_menu();
    return 0;
}