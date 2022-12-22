#include<stdio.h>
#include<conio.h>
#include<stdlib.h>
#include<ctype.h>
#include<direct.h>
#include<process.h>
#include<time.h>
#include<string.h>
#include<windows.h>

// Various User Defined Function

void add();// add a record
void search();// search a record
void list(); //display the record
void del(); // delete the record
void modify();// mpdify the record
void rec(); //scan the new record
void mainpage(); //starting page
int logscreen();// login screen
void callExit();// exit function
void menu();// Main Menu
void date();// to get current date
void empty();// used in deleting the record
void title();// title bar

void delay(unsigned int mseconds)
{
    clock_t goal = mseconds + clock();
    while (goal > clock());
}

/*declaring structure*/

struct information
{
  char ID[15]; //Student ID
  char name[30];  //Student name
  char cls[10];  //Class of Student
  char Branch[35];  //Branch of class
  char address[50];  //Address of Student
  char email[30];  //Email of Student
  char rollno[15];  //Roll No Of a Student
  char phoneno[15];  //Phone No Of a Student
};
struct information c;
FILE *fpt;

void main()
{
  system("cls");
  mainpage();
  logscreen();
}

void date()
{
  time_t T= time(NULL);
  struct  tm tm = *localtime(&T);
  printf("\n\n\n");
  printf("\t\t\t\t\t   Date:%02d/%02d/%04d\n",tm.tm_mday, tm.tm_mon+1, tm.tm_year+1900);
}


void title()
{
  printf("\n\n\n");
  printf("\t\t\t\t\t");
  printf("\n");
  printf("\t\t\t\t    ---------------------------\n");
  printf("\t\t\t\t    |STUDENT MANAGEMENT SYSTEM|\n");
  printf("\t\t\t\t    ---------------------------\n");
  printf("\n\n");
  printf("\t\t\t\t");

}

void mainpage()
{
  int process=0;
  system("cls");
  date();
  printf("\n");
  printf("\t\t\t\t    ---------------------------\n");
  printf("\t\t\t\t    |STUDENT MANAGEMENT SYSTEM|\n");
  printf("\t\t\t\t    ---------------------------\n");
  printf("\n\n\n");
  printf("\t\t\t\t");
  printf("Prepared By    ");
  printf(":");
  printf("   Chetan Khunti");
  printf("\n\n");
  printf("\t\t\t\t");
  printf("Mini Project   ");
  printf(":");
  printf("   Student Management System");
  printf("\n\n");
  printf("\t\t\t\t");
  printf("Guided By      ");
  printf(":");
  printf("   Dr.Amit Lathigara");
  printf("\n\n");
  printf("\t\t\t\t    Press Enter to continue......");
  printf("\n\n");
  getchar();
  printf("\t\t\t\tLoading");

  for(process=0;process<25;process++)
  {
    delay(150);
    printf(".");
  }
}

int logscreen()
{
  char username[30];
  char password[30];
  int try = 0;
  int true = 1;
  do
  {
    system("cls");
    printf("\n\n\n\n\n\n\n\n\n\n");
    printf("\t\t\t\t\t");
    printf("Username: ");
    scanf("%s",username);
    printf("\t\t\t\t\t");
    printf("\n");
    printf("\t\t\t\t\t");
    printf("Password: ");
    scanf("%s",password);
    if(strcmp(username,"admin")==0 && strcmp(password,"pass")==0)
      {
        printf("\n\n");
        printf("\t\t\t\t");
        printf("You are accessed to the system!\n\n");
        printf("\t\t\t\t   Press Enter to continue...");
        fflush(stdin);
        getchar();
        menu();
        system("cls");
        getchar();
        true =0;
      }
      else
      {
        system("cls");
        try = try+1;
        printf("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        printf("\t\t\t\t\t");
        printf("No. of attempts remain: %d",3-try);
        fflush(stdin);
        getchar();
        if(try>=3)
        {

          printf("\t\t\t\t\t\t");
          printf("\n");
          printf("\t\t\t\t");
          printf("   No permission to enter the system!" );
          getchar();
          callExit();
          getchar();
      ;
      }
  } //END OF ELSE
 }
 while(true==1);
}


void menu()
{
  int input;
  system("cls");
  title();
  printf(" 1. Enter new Records\n\n");
  printf("\t\t\t\t");
  printf(" 2. Modify Records\n\n");
  printf("\t\t\t\t");
  printf(" 3. Delete Records\n\n");
  printf("\t\t\t\t");
  printf(" 4. Search and view Records\n\n");
  printf("\t\t\t\t");
  printf(" 5. Exit\n\n");
  printf("\t\t\t\t");
  printf("Choose options:[1/2/3/4/5]:");
  fflush(stdin);
  scanf("%d",&input);
  switch(input)
  {
    case 1:
    {
      system("cls");
      add();
    }
    break;

    case 2:
    {
      system("cls");
      modify();
      getchar();
    }
    break;

    case 3:
    {
      system("cls");
      del();
    }
    break;

    case 4:
    {
      system("cls");
      search();
    }break;

    case 5:
    {
      system("cls");
      printf("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
      fflush(stdin);
      printf("\t\t\t\t");
      printf("Brought To You By code-projects.org");
      printf("\n\n\n\n");
      exit(0);
    }
    break;

    default:
    {
      fflush(stdin);
      printf("\n\n\n");
      printf("\t\t\t\t");
      printf("      Invalid input!");
      printf("\n\n");
      printf("\t\t\t\t");
      printf("Press Enter to choose again...");
      getchar();
      menu();
    }
  }
  }
  void add()
  {
    char input;
    system("cls");
    printf(" ");
    printf("\n");
    printf("\t\t\t\t");
    printf("\n");
    printf("\t\t\t\t");
    printf("\n");
    printf("\t\t\t\t");
    printf("\n");
    printf("\n\n\n\n");
    printf("\t\t\t\t");
    printf("Enter The Information Below: \n\n");
    printf("\t\t\t\t");
    printf("Enter ID        :  ");

    fflush(stdin);
    scanf("%s",c.ID);
    printf("\n\n");
    printf("\t\t\t\t");
    printf("Full Name       :  ");
    fflush(stdin);
    scanf("%[^\n]s",c.name);
    printf("\n\n");
    printf("\t\t\t\t");
    printf("Class           :  ");
    fflush(stdin);
    scanf("%[^\n]s",c.cls);
    printf("\n\n");
    printf("\t\t\t\t");
    printf("Branch          :  ");
    fflush(stdin);
    scanf("%[^\n]s",c.Branch);
    printf("\n\n");
    printf("\t\t\t\t");
    printf("Address         :  ");
    fflush(stdin);
    scanf("%[^\n]s",c.address);
    printf("\n\n");
    printf("\t\t\t\t");
    printf("Email Address   :  ");
    fflush(stdin);
    scanf("%[^\n]s",c.email);
    printf("\n\n");
    printf("\t\t\t\t");
    printf("Roll No.        :  ");
    fflush(stdin);
    scanf("%s",c.rollno);
    printf("\n\n");
    printf("\t\t\t\t");
    printf("Phone No.       :  ");
    fflush(stdin);
    scanf("%s",c.phoneno);
    fflush(stdin);
    fpt=fopen("data.txt","a");
    fwrite(&c,sizeof(c),1,fpt);
    fclose(fpt);
    printf("\n\n");
    printf("\t\t\t\t");
    printf("Record added successfully!!");

    printf("\n\n");
    printf("\t\t\t\t");
    printf("Do you want to add more?(Y/N)");
    scanf("%s",&input);
    if(input=='Y' || input=='y')
    {
      add();
    }
    else
    menu();
    getchar();
    }

      void callExit()
      {
       system("cls");
       printf("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
       printf("\t\t\t\t\t");
       fflush(stdin);
       printf("You are out of the System.");
       printf("\n\n");
       printf("\t\t\t\t");
       printf("   Press Enter to Continue Exit Program...");
       getchar();
       exit(0);
      }

      void search()
      {
        int ch;
        char sid[30];
        system("cls");
        printf("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        printf("\t\t\t\t\t");

        fflush(stdin);
        printf("Enter Full Name: ");
        scanf("%[^\n]s",sid);

        fpt=fopen("data.txt","r");
        while (fread(&c, sizeof(c), 1, fpt)==1)
        {
          if(strcmp(sid,c.name)==0)
          {
            ch=1;
            break;
          }
        }
        if(ch==1)
        {
          system("cls");
          printf("\n\n\n\n\n\n\n");
          printf("\t\t\t\t\t");

          printf("<<==Search Succesfull==>>");
          fflush(stdin);
          getchar();
          list(); //display the record
          printf("\n\n\n");
          printf("\t\t\t\t\t");
          printf("Press Enter For Main Menu...");
          getchar();
          menu();
        }
        else
        {
          system("cls");
          getchar();
          printf("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
          printf("\t\t\t\t\t");

          printf("<<--Record Not Found-->>");
          printf("\n\n\n");
          printf("\t\t\t\t\t");
          printf("Press Enter For Main Menu...");
          getchar();
          menu();
        }

        fclose(fpt);
      }


      void list()
{
      printf(" ");
      printf("\n");
      printf("\t\t\t\t");

      printf("\t\t\t\t");
      printf("\n");
      printf("\t\t\t\t");
      printf("\n");

      printf("\t\t\t\t");


      printf("\tID      :  %s\n\n",c.ID);
      printf("\t\t\t\t");
      printf("Full Name       : %s  ",c.name);

      printf("\n\n");
      printf("\t\t\t\t");
      printf("Class           : %s  ",c.cls);

      printf("\n\n");
      printf("\t\t\t\t");
      printf("Branch          : %s  ",c.Branch);

      printf("\n\n");
      printf("\t\t\t\t");
      printf("Address         : %s  ",c.address);

      printf("\n\n");
      printf("\t\t\t\t");
      printf("Email Address   : %s  ",c.email);

      printf("\n\n");
      printf("\t\t\t\t");
      printf("Roll No.        : %s  ",c.rollno);

      printf("\n\n");
      printf("\t\t\t\t");
      printf("Phone No.       : %s  ",c.phoneno);

}

       void del()
       {
         FILE *fpt,*temp;
         int ch;
         char sid[30];

         system("cls");
         printf("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
         printf("\t\t\t\t\t");
         printf("Enter Full Name:");
         fflush(stdin);
         scanf("%[^\n]s",sid);
         fflush(stdin);
         printf("\t\t\t\t\t");
         printf("\n");
         printf("\t\t\t\t\t");
         printf("Record Deleted Successfully.\n");
         printf("\n");
         printf("\t\t\t\t\t");
         printf("Press Enter For Main Menu...");
         getchar();
         menu();
         fpt=fopen("data.txt","r");
         temp = fopen("temp.txt", "w");
         while (fread(&c, sizeof(c), 1, fpt)==1)
         {
           if(strcmp(sid,c.name)==0)
           {
             {
                 fwrite(&c,sizeof(c),1,temp);
           }
         }
         fclose(fpt);
         fclose(temp);
         remove("data.txt");
         rename("temp.txt","data.txt");
         }
       }

       /*void del()
       {
         int ch;
         char cno[30];
         system("cls");
         fpt=fopen("data.txt","r+");
         printf("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
         printf("\t\t\t\t\t");
         printf("Enter Full Name:");
         fflush(stdin);
         scanf("%[^\n]s",cno);
         fflush(stdin);
         while ( fread(&c, sizeof(c), 1, fpt) && strcmp(cno,c.name) );
         ch=strcmp(cno,c.name);
         if(ch!=0)
         {
           system("cls");
           delay(200);
           printf("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
           printf("\t\t\t\t\t");
           printf("<<--Record Not Found-->>");
           getchar();
           menu();
           }
        else //Here the saved record is displayed.
        {
          system("cls");
          printf("\n\n\n\n\n\n\n\n\n");
          printf("\t\t\t\t\t");
          printf("<<==Search Succesfull==>>");
          list(); //display the record
          printf("\n\n");
          printf("\t\t\t\t\t");
          printf("Press Enter To Delete The Record.....");
          getchar();
          empty(); //this is a Function which will erase the record in memory & NOT physically.
          fseek(fpt, ftell(fpt) - sizeof(c), 0);
          fwrite(&c, sizeof(c), 1, fpt);
          system("cls");
          delay(200);
          printf("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
          printf("\t\t\t\t\t");
          printf("Record Deleted !!!");
          getchar();
        }
        menu();
        fclose(fpt);
      }*/

      void empty()
      {
        strcpy(c.ID," ");
        strcpy(c.name," ");
        strcpy(c.cls," ");
        strcpy(c.Branch," ");
        strcpy(c.address," ");
        strcpy(c.email," ");
        strcpy(c.rollno," ");
        strcpy(c.phoneno," ");
      }

void modify()
{
  char mid[30];

  int found=0;
  printf("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
  printf("\t\t\t\t\t");
  fflush(stdin);
  printf("Enter Full Name: ");
  scanf("%[^\n]s",mid);
  fflush(stdin);
  FILE *fpt, *temp;
  temp =fopen("temp.txt","w");
  fpt = fopen("data.txt","r+");
  while(fread(&c, sizeof(c),1,fpt) == 1)
  {
      if(strcmp(mid, c.name) == 0)
      {
          fflush(stdin);
          system("cls");
          printf("\n\n\n\n\n\n\n\n\n\n\n");
          printf("\t\t\t\t\t");
          printf("<<<==Old Record==>>>");
          printf("\n");
          printf("\t\t\t\t\t");
          list();
          printf("\n\n");
          printf("\t\t\t\t\t");

          printf("Press Enter to modify the record...");


          getchar();
          system("cls");
          rec(); // this will ask to enter new record
          fseek(fpt, ftell(fpt) - sizeof(c),0);
          fwrite(&c, sizeof(c), 1, fpt); //The new name will be added to the record.
          found = 1;
          printf("\n\n");
          printf("\t\t\t\t\t");
          printf("Record Saved !!!");
          getchar();
        }
        fclose(fpt);
        menu();
      }

      if(!found)
      {
        {
          system("cls");
          delay(200);
          fflush(stdin);

          printf("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
          printf("\t\t\t\t\t");
          fflush(stdin);

          printf("<<--Record Not Found-->>");
          getchar();
          menu();
        }
      }
      fclose(fpt);
}
       void rec()
       {
         printf(" ");
         printf("\n");
         printf("\t\t\t\t");
         printf("\n");
         printf("\t\t\t\t");
         printf("\n");
         printf("\t\t\t\t");
         printf("\n");
         printf("\n\n\n\n");
         printf("\t\t\t\t");

         printf("Enter The Information Below: \n\n");
         printf("\t\t\t\t");
         printf("\tID      :  %s\n\n",c.ID);
         printf("\t\t\t\t");
         printf("Full Name       :  ");
         fflush(stdin);
         scanf("%[^\n]s",c.name);
         printf("\n\n");
         printf("\t\t\t\t");
         printf("Class           :  ");
         fflush(stdin);
         scanf("%[^\n]s",c.cls);
         printf("\n\n");
         printf("\t\t\t\t");
         printf("Branch          :  ");
         fflush(stdin);
         scanf("%[^\n]s",c.Branch);
         printf("\n\n");
         printf("\t\t\t\t");
         printf("Address         :  ");
         fflush(stdin);
         scanf("%[^\n]s",c.address);
         printf("\n\n");
         printf("\t\t\t\t");
         printf("Email Address   :  ");
         fflush(stdin);
         scanf("%[^\n]s",c.email);
         printf("\n\n");
         printf("\t\t\t\t");
         printf("Roll No.        :  ");
         fflush(stdin);
         scanf("%s",c.rollno);
         printf("\n\n");
         printf("\t\t\t\t");
         printf("Phone No.       :  ");
         fflush(stdin);
         scanf("%s",c.phoneno);
         fflush(stdin);
}
