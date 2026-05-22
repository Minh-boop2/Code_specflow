# SpecFlow Login Testing Demo

## 1. Gioi thieu

Project nay dung SpecFlow + NUnit + .NET de kiem thu chuc nang dang nhap theo huong BDD.

BDD dung 3 buoc:

- Given: dieu kien ban dau
- When: hanh dong nguoi dung
- Then: ket qua mong doi

Project co comment trong code va co log tren terminal de de biet dang chay step nao.

---

## 2. Cau truc code

| File | Noi dung |
|---|---|
| Features/Login.feature | Viet test case bang Gherkin |
| StepDefinitions/LoginSteps.cs | Noi Given, When, Then voi code C# |
| Services/LoginService.cs | Xu ly logic dang nhap |
| README.md | Huong dan chay va thuyet trinh |

---

## 3. Chay toan bo test

### Lenh chay

    dotnet test

### Code nam o dau?

    Features/Login.feature
    StepDefinitions/LoginSteps.cs
    Services/LoginService.cs

### Mo ta

Lenh nay chay toan bo 8 test case trong project.

### Ket qua dung

    Test summary: total: 8, failed: 0, succeeded: 8, skipped: 0
    Build succeeded

---

## 4. Test dang nhap dung

### Lenh chay

    dotnet test --filter "TestCategory=positive"

### Code nam o dau?

File test case:

    Features/Login.feature

Doan test:

    @positive @smoke
    Scenario: Login successfully with valid account
      Given the user is on the login page
      When the user enters username "admin" and password "123456"
      Then the login result should be "Login successful"

File step:

    StepDefinitions/LoginSteps.cs

Function xu ly:

    WhenTheUserEntersUsernameAndPassword()
    ThenTheLoginResultShouldBe()

File logic:

    Services/LoginService.cs

Function xu ly:

    Login()

### Mo ta

Test nay kiem tra truong hop nhap dung username va password.

Username dung:

    admin

Password dung:

    123456

Ket qua mong doi:

    Login successful

### Tac dung

Chung minh he thong dang nhap thanh cong khi thong tin hop le.

---

## 5. Test loi du lieu

### Lenh chay

    dotnet test --filter "TestCategory=negative"

### Code nam o dau?

File test case:

    Features/Login.feature

Doan test:

    @negative @data-driven
    Scenario Outline: Login validation with multiple input data

File step:

    StepDefinitions/LoginSteps.cs

Function xu ly:

    WhenTheUserEntersUsernameAndPassword()
    ThenTheLoginResultShouldBe()

File logic:

    Services/LoginService.cs

Function xu ly:

    Login()

### Cac truong hop duoc test

- Sai mat khau
- Bo trong username
- Bo trong password
- Sai username
- Sai password khac

### Mo ta

Test nay dung Scenario Outline de chay nhieu bo du lieu trong cung mot kich ban.

### Tac dung

Giup dam bao he thong tra ve dung thong bao loi khi nguoi dung nhap sai du lieu.

---

## 6. Test bao mat khoa tai khoan

### Lenh chay

    dotnet test --filter "TestCategory=security"

### Code nam o dau?

File test case:

    Features/Login.feature

Doan test:

    @security @negative
    Scenario: Account is locked after three failed login attempts

    @security @negative
    Scenario: Locked account cannot login even with correct password

File step:

    StepDefinitions/LoginSteps.cs

Function xu ly:

    WhenTheUserEntersUsernameAndPasswordMultipleTimes()
    WhenTheUserEntersUsernameAndPassword()
    ThenTheLoginResultShouldBe()

File logic:

    Services/LoginService.cs

Function xu ly:

    Login()
    RegisterFailedAttempt()
    Reset()

### Mo ta

Test bao mat kiem tra 2 viec:

1. Sai mat khau 3 lan thi tai khoan bi khoa
2. Tai khoan da bi khoa thi nhap dung mat khau van khong dang nhap duoc

### Tac dung

Giup chung minh he thong co co che bao mat co ban, han che viec thu mat khau nhieu lan.

---

## 7. Xuat file ket qua test

### Lenh chay

    dotnet test --logger "trx;LogFileName=SpecFlowResults.trx"

### File ket qua nam o dau?

    TestResults/SpecFlowResults.trx

### Tac dung

Dung de luu ket qua test lam minh chung cho bao cao hoac thuyet trinh.

---

## 8. Cac diem nang cao

### Scenario Outline

Nam o:

    Features/Login.feature

Tac dung:

- Test nhieu bo du lieu
- Giam lap scenario
- De them test case moi

### Tags

Nam o:

    Features/Login.feature

Cac tag:

    @positive
    @negative
    @security
    @data-driven
    @smoke

Tac dung:

- Phan loai test
- Chay rieng tung nhom test
- De quan ly hon

### Console log

Nam o:

    StepDefinitions/LoginSteps.cs

Tac dung:

- Khi chay test, terminal hien dang chay Given, When, Then nao
- De giai thich khi thuyet trinh
- De debug neu test bi fail

---

## 9. Cau thuyet trinh ngan

Trong project nay, em dung SpecFlow de kiem thu chuc nang dang nhap theo huong BDD. Test case duoc viet trong Features/Login.feature bang cu phap Given, When, Then. Cac step nay duoc noi voi code C# trong StepDefinitions/LoginSteps.cs va goi logic dang nhap trong Services/LoginService.cs.

Project co 3 nhom test: positive, negative va security. Nhom positive test dang nhap dung. Nhom negative test cac loi du lieu. Nhom security test khoa tai khoan sau 3 lan sai mat khau.

Khi chay dotnet test, neu ket qua la total: 8, failed: 0, succeeded: 8 thi toan bo test da chay thanh cong.
