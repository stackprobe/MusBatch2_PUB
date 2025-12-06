// cur repo: https://github.com/stackprobe/Factory/blob/master/Common/all.h
// old repo: https://github.com/stackprobe/Factory_20221106/blob/master/Common/all.h
#include "C:\Dev\Factory\Common\all.h"

#include "..\Random.h"

int main(int argc, char **argv)
{
	InitRandom();

	cout("%08x\n", GetRandom());
	cout("%08x\n", GetRandom());
	cout("%08x\n", GetRandom());
}
