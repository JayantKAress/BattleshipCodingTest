using BattleShipCodingTest.Shared.Exceptions;
using BattleShipStateTracker.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipStateTracker.Common.Service
{
    public class BattleShipService : IBattleShipService
    {
        const int boardSize = 10;
        const int maxShips = 4;
        private string[,] board;
        private string[,] mockBoard;
        private string position = "00";
        private ShipCordinates shipCoordinates;
        private List<Ship> ships = new List<Ship>();

        #region CreateBoard
        public void CreateBoard()
        {
            board = new string[boardSize, boardSize];
            mockBoard = new string[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    board[i, j] = $"[{position}]";
                    mockBoard[i, j] = $"[{position}]";
                }
            }
        }

        public string ShowBoard()
        {
            string displayBoard = BoardArrayToString();
            return displayBoard;
        }

        public string BoardArrayToString()
        {
            StringBuilder sb = new StringBuilder();

            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(board[i, j]);
                    if (j < cols - 1)
                    {
                        sb.Append(", "); // Add a comma between elements in the same row
                    }
                }
                sb.AppendLine(); // Move to the next line after each row
            }

            return sb.ToString();
        }

        public string MockBoardArrayToString()
        {
            StringBuilder sb = new StringBuilder();

            int rows = mockBoard.GetLength(0);
            int cols = mockBoard.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(mockBoard[i, j]);
                    if (j < cols - 1)
                    {
                        sb.Append(", "); // Add a comma between elements in the same row
                    }
                }
                sb.AppendLine(); // Move to the next line after each row
            }

            return sb.ToString();
        }
        #endregion

        #region AddShip
        public bool ValidateShip(int totalShips)
        {
            if (totalShips == 0)
            {
                throw new BattleShipApiException("Total ships cannot be 0");
            }
            else if (totalShips > maxShips)
            {
                throw new BattleShipApiException("Cannot add ships more than 4 ships on 10x10 board");
            }
            return true;
        }

        public void AddShip(int totalShips)
        {
            ValidateShip(totalShips);
            int TotalShips = totalShips;
            for (int i = 0; i < TotalShips; i++)
            {
                Random random = new Random();
                int shipLength = random.Next(2, boardSize);

                Ship objShip = GenerateRandomShipPosition(shipLength);
                while (ValidateShipAdjacent(objShip))
                {
                    objShip = GenerateRandomShipPosition(shipLength);
                }
                ships.Add(objShip);
                UpdateShipOnBoard(objShip);
            }
        }

        private void UpdateShipOnBoard(Ship ShipObj)
        {
            foreach (var position in ShipObj.Positions)
            {
                mockBoard[position.Item1, position.Item2] = $"[{ShipObj.Name}]";
            }
        }

        private Ship GenerateRandomShipPosition(int shipLength)
        {
            Ship ship = new Ship("S" + (ships.Count + 1));
            Random random = new Random();

            bool isHorizontal = random.Next(2) == 0;
            int x = random.Next(0, boardSize);
            int y = random.Next(0, boardSize);

            if (isHorizontal)
            {
                while (x + shipLength > boardSize)
                {
                    x = random.Next(0, boardSize - shipLength);
                }
            }
            else
            {
                while (y + shipLength > boardSize)
                {
                    y = random.Next(0, boardSize - shipLength);
                }
            }

            for (int i = 0; i < shipLength; i++)
            {
                if (isHorizontal)
                {
                    AddPosition(ship, x + i, y);
                }
                else
                {
                    AddPosition(ship, x, y + i);
                }
            }

            return ship;
        }

        private bool ValidateShipAdjacent(Ship newShip)
        {
            foreach (var existingShip in ships)
            {
                foreach (var position in existingShip.Positions)
                {
                    if (newShip.Positions.Contains(position))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void AddPosition(Ship ship, int x, int y)
        {
            ship.Positions.Add((x, y));
        }

        #endregion

        #region FireShip
        public void FireShip(int x, int y)
        {
            shipCoordinates = new ShipCordinates()
            {
                Xcoordinate = x,
                Ycoordinate = y,
            };
        }

        public string HitOrMissShip()
        {
            string hitResult = "You miss the target";
            if (mockBoard[shipCoordinates.Xcoordinate, shipCoordinates.Ycoordinate].Contains("S"))
            {
                board[shipCoordinates.Xcoordinate, shipCoordinates.Ycoordinate] = "[HH]";
                mockBoard[shipCoordinates.Xcoordinate, shipCoordinates.Ycoordinate] = "[HH]";
                hitResult = "You hit the target";
            }
            else
            {
                mockBoard[shipCoordinates.Xcoordinate, shipCoordinates.Ycoordinate] = "[MM]";
                board[shipCoordinates.Xcoordinate, shipCoordinates.Ycoordinate] = "[MM]";
            }
            return hitResult;
        }

        public bool WinGame()
        {
            bool winResult = false;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (mockBoard[i, j] == "[S1]" || mockBoard[i, j] == "[S2]" ||
                        mockBoard[i, j] == "[S3]" || mockBoard[i, j] == "[S4]")
                        return true;
                }
            }
            return winResult;
        }

        #endregion

        #region GameRestart
        public void RestartGame()
        {
            board = new string[boardSize, boardSize];
            mockBoard = new string[boardSize, boardSize];
            ships = new List<Ship>();
        }
        #endregion
    }
}
