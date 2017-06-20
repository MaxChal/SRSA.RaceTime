function LiveViewModel() {
    this.TestText = ko.observable("Bert");
}

//Models
function Collision() {
    this.collisionId;
    this.sessionId;
    this.type;
    this.timestamp;
    this.competitor1;
    this.competitor2;
    this.impactSpeed;
    this.worldPosition;
    this.relPosition;
}

function Competitor() {
    this.competitorId;
    this.sessionId;
    this.driverId;
    this.driverGuid;
    this.driverName;
    this.driverTeam;
    this.carId;
    this.carModel;
    this.carSkin;
    this.ballastKg;
    this.bestLap;
    this.totalTime;
    this.lapCount;
    this.lapsLead;
    this.startPosition;
    this.position;
    this.gap;
    this.incidents;
    this.distance;
    this.currentSpeed;
    this.currentAcceleration;
    this.currentLapStart;
    this.currentTyreCompount;
    this.topSpeed;
    this.startSplinePos;
    this.endSplinePos;
    this.isAdmin;
    this.isConnected;
    this.isOnOutLap;
    this.connectionId;
    this.connectedTimeStamp;
    this.disconnectedTimeStamp;
}

function Driver()
{
    this.driverId;
    this.driverName;
    this.driverAge;
    this.driverLevel;
    this.driverSeries;
}

function Lap() {
    this.lapId;
    this.competitorId;
    this.lapTime;
    this.sector1;
    this.sector2;
    this.sector3;
    this.lapLength;
    this.lapNo;
    this.position;
    this.cuts;
    this.gripLevel;
    this.isValid;
    this.tyreCompound;
    this.connectionId;
    this.timeStamp;
    this.dbTimeStamp;
}

function Pitstop() {
    this.pitstopId;
    this.competitorId;
    this.lapNumber;
    this.timestamp;
}

function Session() {
    this.sessionId;
    this.eventId;
    this.sessionGame;
    this.serverName;
    this.sessionName;
    this.sessionType;
    this.sessionTrack;
    this.sessionLaps;
    this.sessionDuration;
    this.sessionWaitTime;
    this.sessionTrackConfig;
    this.version;
    this.ambientTemp;
    this.roadTemp;
    this.weather;
    this.elapsedMs;
    this.timestamp;
    this.isActive;
    this.sessionIndex;
    this.currentSessionIndex;
    this.sessionCount;
}