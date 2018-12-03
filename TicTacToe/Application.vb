

Public Class Application
    Public Shared Sub Main()
        Dim squares(8) As String
          
        'Set the initial values of the squares
        For i As Integer = 0 To 8
            squares(i) = " "
        Next
        
        Dim playedSquare As Integer
        Dim computerSquare As Integer
        Dim isValidInput As Boolean = False
        Dim computerSymbol As String = "O"
        Dim winner As Boolean = False
        
        'The main game loop
        While BoardIsFull(squares) = False
            System.Console.Clear()
            
            'Write the Board
            WriteBoard(squares)
            
            'User Plays
            System.Console.WriteLine("You are 'X', and the Computer is 'O'.")
            
            
            While True
                'Use Try/Catch to validate user input as an integer              
                System.Console.WriteLine("Please enter the number of the square you would like to play on:")
                Try
                    playedSquare = Integer.Parse(System.Console.ReadLine()) - 1
                    
                    If SquareIsOpen(squares(playedSquare)) Then
                        squares(playedSquare) = "X"
                        System.Console.WriteLine($"You played on square {playedSquare}") 
                        'Break the loop if the player has succesfully played a square
                        Exit While
                    Else 
                        System.Console.WriteLine($"Square {playedSquare} has already been played - try again!")
                        Exit Try
                    End If
                Catch
                    System.Console.WriteLine("Invalid square - you must enter a number between 1 and 9!")
                    System.Console.WriteLine("Press Enter to try again >>")
                    System.Console.ReadLine()
                End Try
            End While 
            
            If BoardIsFull(squares) <> True Then
                'Computer plays
                computerSquare = FindOpenSquare(squares)
                squares(computerSquare) = "O"
                System.Console.WriteLine($"The computer played on square {computerSquare}")
            End If
            
             'Check winning lines
             If WinningLine(squares, computerSymbol) Or WinningLine(squares, "X")
                winner = True
                Exit While
             End If
        End While
        
        'Write the full board one last time
        WriteBoard(squares)
        System.Console.Writeline("Game Over! The Board was completely filled up!")  
    End Sub
    
    Public Shared Sub WriteBoard(squares As String())
        Dim line As String = "-----"
        
        'Write the board to the console'
        System.Console.WriteLine($"{squares(0)}|{squares(1)}|{squares(2)}")
        System.Console.WriteLine($"{line}")
        System.Console.WriteLine($"{squares(3)}|{squares(4)}|{squares(5)}")
        System.Console.WriteLine($"{line}")
        System.Console.WriteLine($"{squares(6)}|{squares(7)}|{squares(8)}")
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
        'Winning lines on the board:'
        '1, 2, 3'
        '4, 5, 6'
        '7, 8, 9'
        '1, 4, 7'
        '2, 5, 8'
        '3, 6, 9'
        '1, 5, 9'
        '3, 5, 7'
        
        'We'll use a 2 dimensional array 
        Dim winningLines = New Integer(7, 2) {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}, {1, 4, 7}, {2, 5, 8}, {3, 6, 9}, {1, 5, 9}, {3, 5, 7}}
        
        For Each line As Integer In winningLines
            'Testing...
            If line(0) = playerSymbol AndAlso line(1) = playerSymbol AndAlso line(2) = playerSymbol
                Return True
            End If          
        Next 

        Return False
    End Function
End Class
