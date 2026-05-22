@login
Feature: Advanced Login Testing

  # File nay viet cac test case bang Gherkin.
  # Moi Scenario la mot chuc nang can kiem thu.
  # Tag dung de chay rieng tung nhom test:
  # @positive  : test dang nhap dung
  # @negative  : test loi du lieu
  # @security  : test bao mat khoa tai khoan

  @positive @smoke
  Scenario: Login successfully with valid account
    # Test chuc nang dang nhap thanh cong
    Given the user is on the login page
    When the user enters username "admin" and password "123456"
    Then the login result should be "Login successful"

  @negative @data-driven
  Scenario Outline: Login validation with multiple input data
    # Test nhieu bo du lieu sai bang Scenario Outline
    Given the user is on the login page
    When the user enters username "<username>" and password "<password>"
    Then the login result should be "<message>"

    Examples:
      | username | password | message                      |
      | admin    | wrong    | Invalid username or password |
      |          | 123456   | Username is required         |
      | admin    |          | Password is required         |
      | user     | 123456   | Invalid username or password |
      | admin    | 12345    | Invalid username or password |

  @security @negative
  Scenario: Account is locked after three failed login attempts
    # Test bao mat: sai mat khau 3 lan thi khoa tai khoan
    Given the user is on the login page
    When the user enters username "admin" and password "wrong" 3 times
    Then the login result should be "Account is locked"

  @security @negative
  Scenario: Locked account cannot login even with correct password
    # Test bao mat: tai khoan da khoa thi nhap dung van khong dang nhap duoc
    Given the user is on the login page
    When the user enters username "admin" and password "wrong" 3 times
    And the user enters username "admin" and password "123456"
    Then the login result should be "Account is locked"
