#include <fstream>
#include <string.h>
#include <bits/stdc++.h>
// #include <string>
// #include <Windows.h>
// #include <stdlib.h>
#include "admin.h"
#include "main_menu.h" 


using namespace std;

void viewData_student(void)

void studentFunction(){
    system("cls");

    cout << "\n\n\t\t\t\t\t | Dang nhap voi vai tro sinh vien |\n";
    cout << "\n\n\t\t\t\t\t 1. Xem bang du lieu ";
    cout << "\n\n\t\t\t\t\t 2. Tra cuu du lieu ";
    cout << "\n\n\t\t\t\t\t 3. Man hinh chinh ";
    cout << "\n\n\t\t\t\t\t 4. Thoat ";
    int tuy_chon;
    cout << "\n\n\t\t\t\t\t Moi ban chon so : ";
    do
    {
        cin >> tuy_chon;
        {
            switch (tuy_chon)
            {
            case 1:
                viewData_student();
                break;
            case 2:
                //filterFile();
                studentFunction();
                break;
            case 3:
                mainMenu();
                break;
            case 4:
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
                break;
            }
        }

    } 
    while (tuy_chon != '3');
    
}

void viewData_student()
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
    studentFunction();
}
