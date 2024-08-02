using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Snake
    {

        //1.头
        private (int x, int y) head;
        public (int X, int Y) Head => head;

        //2.身体每一个部位的坐标
        private List<(int x, int y)> body;
        public IEnumerable<(int X, int Y)> Body => body.AsReadOnly();

        //3.前进方向
        private MoveDirection moveDirection;
        //4.新方向
        private MoveDirection newMoveDirection;
        public MoveDirection MoveDirection
        {
            get => moveDirection;
            set
            {
                if (!Enum.IsDefined(typeof(MoveDirection), value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "不是有效的方向值!");
                }
                if ((moveDirection == MoveDirection.Up && value == MoveDirection.Down)
                    || (moveDirection == MoveDirection.Down && value == MoveDirection.Up)
                    || (moveDirection == MoveDirection.Left && value == MoveDirection.Right)
                    || (moveDirection == MoveDirection.Right && value == MoveDirection.Left))
                {
                    return;
                }
                newMoveDirection = value;
            }
        }

        //6.状态（是否死亡）
        private bool isDead;
        public bool IsDead { get => isDead; }

        //7.是否需要添加新身体
        public bool NeedBodySetment { get; set; }

        public Snake(MoveDirection direction, int headX, int headY, int length)
        {
            moveDirection = direction;
            head = (headX, headY);
            body = direction switch
            {
                MoveDirection.Up => Enumerable.Range(1, length).Select(v => (headX, headY + v * 1)).ToList(),
                MoveDirection.Down => Enumerable.Range(1, length).Select(v => (headX, headY + v * -1)).ToList(),
                MoveDirection.Left => Enumerable.Range(1, length).Select(v => (headX, headY + v * 1)).ToList(),
                MoveDirection.Right => Enumerable.Range(1, length).Select(v => (headX, headY + v * -1)).ToList(),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), "初始化方向错误!")
            };
        }

        public bool NextStep()
        {
            if (isDead) { return false; }

            body.Insert(0, head);
            head = newMoveDirection switch
            {
                MoveDirection.Up => (head.x, head.y - 1),
                MoveDirection.Down => (head.x, head.y + 1),
                MoveDirection.Left => (head.x - 1, head.y),
                MoveDirection.Right => (head.x + 1, head.y),
                _ => throw new ArgumentOutOfRangeException(nameof(newMoveDirection), "新方向错误!")
            };

            if (!NeedBodySetment)
                body.RemoveAt(body.Count - 1);

            NeedBodySetment = false;
            moveDirection = newMoveDirection;
            isDead = body.Contains(head);

            return !isDead;
        }

        public void MakeDead()
        {
            isDead = true;
        }
    }
}
