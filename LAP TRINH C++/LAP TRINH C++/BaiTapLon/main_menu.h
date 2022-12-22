#include <fstream>
#include <string.h>
#include <bits/stdc++.h>
// #include <string>
// #include <Windows.h>
// #include <stdlib.h>
#include"admin.h"
#include"student.h"


using namespace std;

//void mainMenu(void);
void admin(void);
void studentFunction(void);

void mainMenu()
{
    system("cls");

    cout << "\n\n\n\n\t\t\t\t\t Dang nhap vơi tu cach : ";
    cout << "\n\n\n\t\t\t\t\t 1. Quan tri vien ";
    cout << "\n\n\t\t\t\t\t 2. Hoc sinh / sinh vien";
    cout << "\n\n\t\t\t\t\t 3. Thoat";
    cout << "\n\n\n\t\t\t\t\t Moi ban chon so : ";

    int choice;
    cin >> choice;
    switch (choice)
    {
    case 1:
        system("cls");

        cout << " Chao mung ban dang nhap voi tu cach quan tri vien"
        admin();
        break;

    case 2:
        system("cls");

        cout << " Chao mung ban dang nhap voi tu cach hoc sinh";
        studentFunction();
        break;

    case 3:
        system("cls");
        cout << "\t\t\n\n\n\n\n\n\n\t\t\t\t\tThoat chương trinh";

        for (int i = 0; i < 4; i++)
        {

            Sleep(1000);
            cout << ".";
        }

        exit(0);
        break;

    default:
        cout << "Ban da nhap sai !";
    }
    mainMenu();
}


