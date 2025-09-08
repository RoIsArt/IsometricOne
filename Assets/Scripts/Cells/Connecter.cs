using System;
using System.Collections.Generic;
using System.Linq;
using DatasAndConfigs;
using NUnit.Framework;
using UnityEngine;

namespace Cells
{
    public class Connecter
    {
        private Cell _cell;
       
        public Connecter(CellData data, Cell thisCell)
        {
            _cell = thisCell;
            
            foreach (SideName side in data.type.GetSides()) 
                Sides[side] = null;
        }

        public Dictionary<SideName, Cell> Sides { get; } = new();
        public bool ContainSide(SideName sideName) => Sides.ContainsKey(sideName);

        public void Connect(SideName sideName, Cell forConnect)
        {
            if (ContainSide(sideName))
                Sides[sideName] = forConnect;
        }
        
        public void ConnectBidirectional(Cell otherCell)
        {
            SideName sideName = GetSideFromCellToDirection(otherCell);
            if (!ContainSide(sideName) || otherCell == null) return;
            
            Connect(sideName, otherCell);
            
            SideName? oppositeSide = sideName.GetOpposite();
            if (otherCell.Connecter.ContainSide(oppositeSide.Value))
            {
                otherCell.Connecter.Connect(oppositeSide.Value, _cell);
            }
        }

        private SideName GetSideFromCellToDirection(Cell to) => 
            DirectionToCell(to).ToSide();

        private Vector2Int DirectionToCell(Cell to) => 
            to.Index - _cell.Index;

        public void Disconnect(SideName sideName)
        {
            if (ContainSide(sideName))
                Sides[sideName] = null;
        }

        public void DisconnectAll()
        {
            foreach (SideName key in Sides.Keys)
            {
                Sides[key] = null;
            }
        }
    }
}