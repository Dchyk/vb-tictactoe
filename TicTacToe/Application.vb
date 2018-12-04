'Tic Tac Toe for Visual Basic

Public Class Application
    Public Shared Sub Main()
    
    Dim playAgain As String = "Y"
    Dim totalScorePlayer As Integer = 0
    Dim totalScoreComputer As Integer = 0
    
        While playAgain <> "N"
        
            Dim squares(8) As String
              
            'Set the initial values of the squares
            For i As Integer = 0 To 8
                squares(i) = " "
            Next
            
            Dim playedSquare As Integer
            Dim computerSquare As Integer
            
            Dim isValidInput As Boolean = False
            
            Dim computerSymbol As String = "O"
            Dim playerSymbol As String = "X"
            
            Dim someoneWon As Boolean = False
            Dim winner As String = "Noone"

            'The main game loop
            While Not BoardIsFull(squares) And Not someoneWon
            
                WriteBoard(squares, totalScorePlayer, totalScoreComputer)
                System.Console.WriteLine("You are 'X', and the Computer is 'O'.")
                
                While True
                    'Use Try/Catch to validate user input as an integer              
                    System.Console.WriteLine("Please enter the number of the square you would like to play on (1-9):")
                    Try
                        playedSquare = Integer.Parse(System.Console.ReadLine()) - 1 'Square 1 is index 0
                        
                        If SquareIsOpen(squares(playedSquare)) Then
                            squares(playedSquare) = playerSymbol
                            System.Console.WriteLine($"You played on square {playedSquare + 1}")
                            PauseForEffect()
                            'Break the while loop if the player has succesfully played a square
                            Exit While
                        Else 
                            System.Console.WriteLine($"Square {playedSquare + 1} has already been played - try again!")
                            Exit Try
                        End If
                    Catch
                        System.Console.WriteLine("Invalid square - you must enter a number between 1 and 9!")
                        System.Console.WriteLine("Press Enter to try again >>")
                        System.Console.ReadLine()
                    End Try
                End While 
                
                If Not BoardIsFull(squares) Then
                    computerSquare = FindOpenSquare(squares)
                    squares(computerSquare) = computerSymbol
                    System.Console.WriteLine($"The computer played on square {computerSquare + 1}")
                    PauseForEffect()
                End If
                
                If WinningLine(squares, computerSymbol) Then 
                   winner = "Computer"
                   totalScoreComputer += 1
                   Exit While
                Else If WinningLine(squares, playerSymbol) = True Then
                   winner = "You"
                   totalScorePlayer += 1
                   Exit While
                End If
               
            End While
            
            System.Console.Clear()
            WriteBoard(squares, totalScorePlayer, totalScoreComputer) 'Display the final board
            
            If winner <> "Noone" Then
                System.Console.WriteLine($"Game Over! {winner} won!")
            Else 
                System.Console.Writeline("Game Over! The Board was completely filled up and noone won!")
            End If
            
            System.Console.WriteLine("Do you want to play again? Y/N:")
            playAgain = System.Console.ReadLine()
        End While    
        
    End Sub
    
    Public Shared Sub PauseForEffect()
        'This raises a warning:
        'Access of shared member, constant member, enum member or nested type through an instance
        System.Threading.Thread.CurrentThread.Sleep(1000)
    End Sub
    
    Public Shared Sub WriteBoard(squares As String(), playerTotal As Integer, computerTotal As Integer)
        Dim line As String = "-----"
        
        System.Console.Clear()
        System.Console.WriteLine($"Player Wins: {playerTotal} | Computer Wins: {computerTotal}")
        System.Console.WriteLine("")
        
        'Write the board to the console'
        System.Console.WriteLine($"{squares(0)}|{squares(1)}|{squares(2)}")
        System.Console.WriteLine($"{line}")
        System.Console.WriteLine($"{squares(3)}|{squares(4)}|{squares(5)}")
        System.Console.WriteLine($"{line}")
        System.Console.WriteLine($"{squares(6)}|{squares(7)}|{squares(8)}")
        System.Console.WriteLine("")
    End Sub
    
    Public Shared Function SquareIsOpen(square As String) As Boolean
        Return If (square = " ", True, False)
    End Function
    
    Public Shared Function BoardIsFull(squares As String()) As Boolean
        For Each square As String In squares
            If square = " " Then
                Return False
            End If
        Next
        
        Return True
    End Function
    
    Public Shared Function FindOpenSquare(squares As String()) As Integer
        'Generate a random number between 1 and 9
        'If it is open, return it. If not, keep trying
        Dim found As Boolean = False
        Dim random As System.Random = new System.Random
        Dim testedSquare As Integer
        
        While found <> True
            testedSquare = random.Next(1, 9)
            
            If SquareIsOpen(squares(testedSquare)) Then
                found = True
            End If            
        End While
        
        Return testedSquare
    End Function
    
    Public Shared Function WinningLine(squares As String(), playerSymbol As String) As Boolean
        'All possible winning lines on the board:'
        '1, 2, 3'
        '4, 5, 6'
        '7, 8, 9'
        '1, 4, 7'
        '2, 5, 8'
        '3, 6, 9'
        '1, 5, 9'
        '3, 5, 7'
        
        Dim winningLineFound As Boolean = False
        Dim squaresPlayedInARow As Integer
        
        'We'll use a 2 dimensional array 
        Dim winningLines(,) As Integer = New Integer(7, 2) {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}, {1, 4, 7}, {2, 5, 8}, {3, 6, 9}, {1, 5, 9}, {3, 5, 7}}
        Dim currentSquare As Integer
        
        For i As Integer = 0 To 7
            squaresPlayedInARow = 0
            For j As Integer = 0 To 2
                currentSquare = winningLines(i, j) - 1 'Subtract 1 to find the correct index
                If squares(currentSquare) = playerSymbol Then
                    squaresPlayedInARow += 1
                End If    
            Next
            
            If squaresPlayedInARow = 3 Then
                Return True
            End If
        Next
        
        Return False
    End Function
    
End Class
